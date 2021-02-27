using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using Moq;
using shop;
using shop.Controllers;
using shop.Models;
using shop.Utility;
using Xunit;

namespace UnitTests
{
    public class HomeControllerTest
    {
        // Test makes sure that Index() method returns correct view.
        // View is correct when its name is not given or if its name is Index
        [Fact(DisplayName = "Index should return default view")]
        public void Index_ShouldReturnDefaultView()
        {
            using (var homeController = new HomeController(MockData.MoqLogger(), MockData.MoqShopContext(),
                MockData.MoqEmailSender(), MockData.MoqMyLogger()))
            {
                var result = homeController.Index() as ViewResult;

                Assert.NotNull(result);
                Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
                Assert.IsAssignableFrom<ViewResult>(result);
            }
        }

        // Test makes sure that model is given to view and it is not null.
        // It also checks that items collections are not null and these collections contain data.
        [Fact(DisplayName = "Index should return valid model")]
        public void Index_ShouldReturnValidModel()
        {
            using (var homeController = new HomeController(MockData.MoqLogger(), MockData.MoqShopContext(),
                MockData.MoqEmailSender(), MockData.MoqMyLogger()))
            {
                var result = homeController.Index() as ViewResult;
                var model = result?.Model as BookGenrePublisher;

                Assert.NotNull(model);
                Assert.NotNull(model.Books);
                Assert.NotNull(model.Genres);
                Assert.NotNull(model.Publishers);

                Assert.Equal(6, model.Genres.Count());
                Assert.Equal(6, model.Publishers.Count());
                Assert.Equal(11, model.Books.Count());

                Assert.Equal("Little Brown Book Group",
                    model.Books.Single(x => x.BookId == 11).Publisher.PublisherName);
            }
        }

        // Test makes sure that model is given to view and it is not null.
        // It also checks that items collections are not null and these collections contain data.
        [Theory(DisplayName = "BookDetails should return valid model")]
        [InlineData(1)]
        public void BookDetails_ShouldReturnValidModel(int bookId)
        {
            using (var homeController = new HomeController(MockData.MoqLogger(), MockData.MoqShopContext(),
                MockData.MoqEmailSender(), MockData.MoqMyLogger()))
            {
                var result = homeController.BookDetails(bookId) as ViewResult;
                var model = result?.Model as Book;

                Assert.NotNull(model);

                Assert.Equal(1, model.BookId);
                Assert.Equal("C# (C Sharp Programming) : A Step by Step Guide for Beginners", model.Title);
            }
        }

        [Theory(DisplayName = "BooksByGenre should return valid model")]
        [InlineData(1)]
        public void BooksByGenre_ShouldReturnValidModel(int genreId)
        {
            using (var homeController = new HomeController(MockData.MoqLogger(), MockData.MoqShopContext(),
                MockData.MoqEmailSender(), MockData.MoqMyLogger()))
            {
                var result = homeController.BooksByGenre(genreId) as ViewResult;
                var model = result?.Model as IEnumerable<Book>;

                Assert.NotNull(model);

                Assert.Equal("Computing", model.FirstOrDefault(x => x.Genre.GenreId == genreId)?.Genre.GenreName);
                Assert.Equal(2, model.Count());
            }
        }

        [Theory(DisplayName = "BooksByPublisher should return valid model")]
        [InlineData(2)]
        public void BooksByPublisher_ShouldReturnValidModel(int publisherId)
        {
            using (var homeController = new HomeController(MockData.MoqLogger(), MockData.MoqShopContext(),
                MockData.MoqEmailSender(), MockData.MoqMyLogger()))
            {
                var result = homeController.BooksByPublisher(publisherId) as ViewResult;
                var model = result?.Model as IEnumerable<Book>;

                Assert.NotNull(model);

                Assert.Equal("Neustar, Inc.",
                    model.FirstOrDefault(x => x.Publisher.PublisherId == publisherId)?.Publisher.PublisherName);
                Assert.Equal(3, model.Count());
            }
        }

        [Fact(DisplayName = "RedirectToBooksByGenre should return valid Controller, Action and RouteValue")]
        public void RedirectToBooksByGenre_CheckRedirectToBooksByGenre()
        {
            using (var homeController = new HomeController(MockData.MoqLogger(), MockData.MoqShopContext(),
                MockData.MoqEmailSender(), MockData.MoqMyLogger()))
            {
                var formCollection = new FormCollection(new Dictionary<string, StringValues>()
                {
                    {"Genres", "1"}
                });

                var controllerContext = new ControllerContext()
                {
                    HttpContext = Mock.Of<HttpContext>(ctx => ctx.Request.Form == formCollection)
                };

                homeController.ControllerContext = controllerContext;

                var action = homeController.RedirectToBooksByGenre() as RedirectToActionResult;

                // action.Should().Be("Home");
                Assert.Equal("BooksByGenre", action.ActionName);
                Assert.Equal("Home", action.ControllerName);
                Assert.Equal("1", action.RouteValues["genreId"].ToString());
            }
        }

        [Fact(DisplayName = "RedirectToBooksByPublishers should return valid Controller, Action and RouteValue")]
        public void RedirectToBooksByPublishers_CheckRedirectToBooksByPublisher()
        {
            using (var homeController = new HomeController(MockData.MoqLogger(), MockData.MoqShopContext(),
                MockData.MoqEmailSender(), MockData.MoqMyLogger()))
            {
                var formCollection = new FormCollection(new Dictionary<string, StringValues>()
                {
                    {"Publishers", "1"}
                });

                var controllerContext = new ControllerContext()
                {
                    HttpContext = Mock.Of<HttpContext>(ctx => ctx.Request.Form == formCollection)
                };

                homeController.ControllerContext = controllerContext;

                var action = homeController.RedirectToBooksByPublishers() as RedirectToActionResult;

                Assert.Equal("BooksByPublisher", action.ActionName);
                Assert.Equal("Home", action.ControllerName);
                Assert.Equal("1", action.RouteValues["publisherId"].ToString());
            }
        }

        [Theory(DisplayName = "AddBooksToSessionBasket checking adding items to an empty basket")]
        [InlineData(1, 2)]
        [InlineData(2, 6)]
        [InlineData(10, 5)]
        public void AddBooksToSessionBasket_CheckAddItemToEmptyBasket(int id, int quantity)
        {
            using (var homeController = new HomeController(MockData.MoqLogger(), MockData.MoqShopContext(),
                MockData.MoqEmailSender(), MockData.MoqMyLogger()))
            {
                OrderedBook orderedBook = new OrderedBook {BookId = id, Quantity = quantity};
                List<OrderedBook> expectedOrderedBookList = new List<OrderedBook>();
                expectedOrderedBookList.Add(orderedBook);

                var controllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() {Session = new MockHttpSession()}
                };

                homeController.ControllerContext = controllerContext;

                var result = homeController.AddBooksToSessionBasket(id, quantity).ToList();

                int actualQuantity = result.FirstOrDefault(x => x.BookId == id)?.Quantity ?? 0;
                int actualId = result.FirstOrDefault(x => x.BookId == id)?.BookId ?? 0;

                Assert.Equal(id, actualId);
                Assert.Equal(quantity, actualQuantity);
            }
        }

        [Theory(DisplayName = "AddBooksToSessionBasket checking adding items to a non-empty basket")]
        [InlineData(1, 2)]
        [InlineData(2, 6)]
        [InlineData(4, 3)]
        [InlineData(6, 0)]
        [InlineData(3, -1)]
        public void AddBooksToSessionBasket_CheckAddItemToNonEmptyBasket(int id, int quantity)
        {
            using (var homeController = new HomeController(MockData.MoqLogger(), MockData.MoqShopContext(),
                MockData.MoqEmailSender(), MockData.MoqMyLogger()))
            {
                OrderedBook orderedBook1 = new OrderedBook {BookId = 1, Quantity = 3};
                OrderedBook orderedBook2 = new OrderedBook {BookId = 2, Quantity = 4};
                OrderedBook orderedBook3 = new OrderedBook {BookId = 3, Quantity = 1};
                List<OrderedBook> mockOrderedBookList = new List<OrderedBook>();
                mockOrderedBookList.Add(orderedBook1);
                mockOrderedBookList.Add(orderedBook2);
                mockOrderedBookList.Add(orderedBook3);

                int quantityFromSession = mockOrderedBookList.FirstOrDefault(x => x.BookId == id)?.Quantity ?? 0;

                var controllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() {Session = new MockHttpSession()}
                };

                homeController.ControllerContext = controllerContext;

                homeController.HttpContext.Session.Set(WebConst.SessionCart, mockOrderedBookList);

                var result = homeController.AddBooksToSessionBasket(id, quantity).ToList();

                int actualQuantity = result.FirstOrDefault(x => x.BookId == id)?.Quantity ?? 0;
                int actualId = result.FirstOrDefault(x => x.BookId == id)?.BookId ?? 0;

                if (actualId == 0 && actualQuantity == 0)
                {
                    Assert.True(mockOrderedBookList.Count == result.Count + 1);
                }
                else
                {
                    Assert.Equal(id, actualId);
                    Assert.Equal(quantity + quantityFromSession, actualQuantity);
                }
            }
        }

        [Theory(DisplayName = "Checking the redirection to the Index after adding the product to the cart")]
        [InlineData(1, 1)]
        [InlineData(2, 3)]
        public void IndexPost_CheckRedirectionAndSession(int id, int quantity)
        {
            using (var homeController = new HomeController(MockData.MoqLogger(), MockData.MoqShopContext(),
                MockData.MoqEmailSender(), MockData.MoqMyLogger()))
            {
                var controllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() {Session = new MockHttpSession()}
                };

                homeController.ControllerContext = controllerContext;

                var action = homeController.IndexPost(id, quantity) as RedirectToActionResult;

                Assert.Equal("Index", action.ActionName);
            }
        }

        [Theory(DisplayName = "Checking the redirection to the BookDetails after adding the product to the cart")]
        [InlineData(2, 4)]
        [InlineData(3, 1)]
        public void BookDetailsPost_CheckRedirectionAndSession(int id, int quantity)
        {
            using (var homeController = new HomeController(MockData.MoqLogger(), MockData.MoqShopContext(),
                MockData.MoqEmailSender(), MockData.MoqMyLogger()))
            {
                var controllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() {Session = new MockHttpSession()}
                };

                homeController.ControllerContext = controllerContext;

                var action = homeController.BookDetailsPost(id, quantity) as RedirectToActionResult;

                Assert.Equal("BookDetails", action.ActionName);
            }
        }

        [Theory(DisplayName = "Checking the redirection to the BooksByGenre after adding the product to the cart")]
        [InlineData(1, 4, 2)]
        [InlineData(4, 1, 3)]
        public void BooksByGenrePost_CheckRedirectionAndSession(int id, int quantity, int gId)
        {
            using (var homeController = new HomeController(MockData.MoqLogger(), MockData.MoqShopContext(),
                MockData.MoqEmailSender(), MockData.MoqMyLogger()))
            {
                var formCollection = new FormCollection(new Dictionary<string, StringValues>()
                {
                    {"Genres", gId.ToString()}
                });

                var controllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext
                    {
                        Session = new MockHttpSession(),
                        Request = {Form = formCollection}
                    },
                };

                homeController.ControllerContext = controllerContext;


                var action = homeController.BooksByGenrePost(id, quantity, gId) as RedirectToActionResult;

                Assert.Equal("BooksByGenre", action.ActionName);
                Assert.Equal(gId.ToString(), action.RouteValues["genreId"].ToString());
            }
        }

        [Theory(DisplayName = "Checking the redirection to the BooksByPublisher after adding the product to the cart")]
        [InlineData(2, 3, 4)]
        [InlineData(5, 2, 2)]
        [InlineData(1, 4, 1)]
        public void BooksByPublisherPost_CheckRedirectionAndSession(int id, int quantity, int pId)
        {
            using (var homeController = new HomeController(MockData.MoqLogger(), MockData.MoqShopContext(),
                MockData.MoqEmailSender(), MockData.MoqMyLogger()))
            {
                var formCollection = new FormCollection(new Dictionary<string, StringValues>()
                {
                    {"Publishers", pId.ToString()}
                });

                var controllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext
                    {
                        Session = new MockHttpSession(),
                        Request = {Form = formCollection}
                    },
                };

                homeController.ControllerContext = controllerContext;


                var action = homeController.BooksByPublisherPost(id, quantity, pId) as RedirectToActionResult;

                Assert.Equal("BooksByPublisher", action.ActionName);
                Assert.Equal(pId.ToString(), action.RouteValues["publisherId"].ToString());
            }
        }

        [Fact(DisplayName = "GetListFromCookies if not empty return list")]
        public void GetListFromCookies_IfNotEmptyReturnList()
        {
            using (var homeController = new HomeController(MockData.MoqLogger(), MockData.MoqShopContext(),
                MockData.MoqEmailSender(), MockData.MoqMyLogger()))
            {
                OrderedBook orderedBook1 = new OrderedBook {BookId = 1, Quantity = 3};
                OrderedBook orderedBook2 = new OrderedBook {BookId = 2, Quantity = 4};
                OrderedBook orderedBook3 = new OrderedBook {BookId = 3, Quantity = 1};
                List<OrderedBook> mockOrderedBookList = new List<OrderedBook>();
                mockOrderedBookList.Add(orderedBook1);
                mockOrderedBookList.Add(orderedBook2);
                mockOrderedBookList.Add(orderedBook3);

                var controllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() {Session = new MockHttpSession()}
                };

                homeController.ControllerContext = controllerContext;
                homeController.HttpContext.Session.Set(WebConst.SessionCart, mockOrderedBookList);

                var result = homeController.GetListFromCookies();

                Assert.Equal(3, result.Count);
                Assert.Equal(4, result.FirstOrDefault(x => x.BookId == 2)?.Quantity ?? 0);
            }
        }

        [Fact(DisplayName = "GetListFromCookies if empty return null")]
        public void GetListFromCookies_IfEmptyReturnNull()
        {
            using (var homeController = new HomeController(MockData.MoqLogger(), MockData.MoqShopContext(),
                MockData.MoqEmailSender(), MockData.MoqMyLogger()))
            {
                List<OrderedBook> emptyOrderedBookList = new List<OrderedBook>();

                var controllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() {Session = new MockHttpSession()}
                };

                homeController.ControllerContext = controllerContext;
                homeController.HttpContext.Session.Set(WebConst.SessionCart, emptyOrderedBookList);

                var result = homeController.GetListFromCookies();

                Assert.Null(result);
            }
        }

        [Fact(DisplayName = "GetListFromCookies if null return null")]
        public void GetListFromCookies_IfNullReturnNull()
        {
            using (var homeController = new HomeController(MockData.MoqLogger(), MockData.MoqShopContext(),
                MockData.MoqEmailSender(), MockData.MoqMyLogger()))
            {
                var controllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() {Session = new MockHttpSession()}
                };

                homeController.ControllerContext = controllerContext;
                homeController.HttpContext.Session.Set(WebConst.SessionCart, (List<OrderedBook>) null);

                var result = homeController.GetListFromCookies();

                Assert.Null(result);
            }
        }

        [Fact(DisplayName = "ShoppingCart if null redirect to Index")]
        public void ShoppingCart_IfNullRedirectToIndex()
        {
            using (var homeController = new HomeController(MockData.MoqLogger(), MockData.MoqShopContext(),
                MockData.MoqEmailSender(), MockData.MoqMyLogger()))
            {
                var controllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() {Session = new MockHttpSession()}
                };

                homeController.ControllerContext = controllerContext;
                homeController.HttpContext.Session.Set(WebConst.SessionCart, (List<OrderedBook>) null);

                var action = homeController.ShoppingCart() as RedirectToActionResult;

                Assert.Equal("Index", action.ActionName);
            }
        }

        [Fact(DisplayName = "ShoppingCart should return valid model")]
        public void ShoppingCart_ShouldReturnValidModel()
        {
            using (var homeController = new HomeController(MockData.MoqLogger(), MockData.MoqShopContext(),
                MockData.MoqEmailSender(), MockData.MoqMyLogger()))
            {
                OrderedBook orderedBook1 = new OrderedBook {BookId = 1, Quantity = 3};
                OrderedBook orderedBook2 = new OrderedBook {BookId = 2, Quantity = 4};
                OrderedBook orderedBook3 = new OrderedBook {BookId = 3, Quantity = 2};
                List<OrderedBook> mockOrderedBookList = new List<OrderedBook>();
                mockOrderedBookList.Add(orderedBook1);
                mockOrderedBookList.Add(orderedBook2);
                mockOrderedBookList.Add(orderedBook3);

                var controllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() {Session = new MockHttpSession()}
                };

                homeController.ControllerContext = controllerContext;
                homeController.HttpContext.Session.Set(WebConst.SessionCart, mockOrderedBookList);

                var result = homeController.ShoppingCart() as ViewResult;
                var model = result?.Model as ShoppingCartViewModel;

                Assert.NotNull(model);
                Assert.NotNull(model.Basket);
                Assert.Equal("The House in the Cerulean Sea", model.Basket.FirstOrDefault(x => x.BookId == 3)?.Title);
                Assert.Equal(2, model.Basket.FirstOrDefault(x => x.BookId == 3)?.Quantity);
                Assert.Equal(70.1, model.Basket.FirstOrDefault(x => x.BookId == 3)?.Price);
            }
        }

        [Theory(DisplayName =
            "ShoppingCartUpdate checking the redirection to the ShoppingCart after updating the product to the cart")]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(4, -1)]
        [InlineData(3, -4)]
        [InlineData(2, -4)]
        public void ShoppingCartUpdate_CheckRedirection(int id, int value)
        {
            using (var homeController = new HomeController(MockData.MoqLogger(), MockData.MoqShopContext(),
                MockData.MoqEmailSender(), MockData.MoqMyLogger()))
            {
                OrderedBook orderedBook1 = new OrderedBook {BookId = 1, Quantity = 3};
                OrderedBook orderedBook2 = new OrderedBook {BookId = 2, Quantity = 4};
                OrderedBook orderedBook3 = new OrderedBook {BookId = 4, Quantity = 2};
                OrderedBook orderedBook4 = new OrderedBook {BookId = 3, Quantity = 3};
                List<OrderedBook> mockOrderedBookList = new List<OrderedBook>();
                mockOrderedBookList.Add(orderedBook1);
                mockOrderedBookList.Add(orderedBook2);
                mockOrderedBookList.Add(orderedBook3);
                mockOrderedBookList.Add(orderedBook4);

                var controllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() {Session = new MockHttpSession()}
                };

                homeController.ControllerContext = controllerContext;
                homeController.HttpContext.Session.Set(WebConst.SessionCart, mockOrderedBookList);

                var action = homeController.ShoppingCartUpdate(id, value) as RedirectToActionResult;

                Assert.Equal("ShoppingCart", action.ActionName);
            }
        }

        [Fact(DisplayName = "ShoppingCartViewModel check methods TotalPrice and TotalAmount")]
        public void ShoppingCartViewModel_CheckMethods()
        {
            ShoppingCartViewModel scvm = new ShoppingCartViewModel
            {
                Basket = MockData.GetMoqOrderedBooksList().ToList()
            };

            double totalPrice = scvm.TotalPrice();
            double totalAmount = scvm.TotalAmount();

            Assert.Equal(10, totalAmount);
            Assert.Equal(906.94, totalPrice);
        }
        
        [Fact(DisplayName = "AddToAddressDBOrGetID address exists")]
        public void AddToAddressDBOrGetID_AddressExists()
        {
            using (var homeController = new HomeController(MockData.MoqLogger(), MockData.MoqShopContext(),
                MockData.MoqEmailSender(), MockData.MoqMyLogger()))
            {
                var address1 = new Address
                {
                    Street = "Spacerowa 23",
                    City = "Warsaw",
                    Country = "Poland",
                    ZipCode = "12-678"
                };

                var address2 = new Address
                {
                    Street = "Pl. Zwyciestwa 12",
                    City = "Cracow",
                    Country = "Poland",
                    ZipCode = "12-345"
                };

                var result1 = homeController.AddToAddressDBOrGetID(address1);
                var result2 = homeController.AddToAddressDBOrGetID(address2);
                
                Assert.Equal(4, result1);
                Assert.Equal(2, result2);
            }
        }
        
        [Fact(DisplayName = "AddToAddressDBOrGetID address do not exists")]
        public void AddToAddressDBOrGetID_AddressNotExists()
        {
            using (var homeController = new HomeController(MockData.MoqLogger(), MockData.MoqShopContext(),
                MockData.MoqEmailSender(), MockData.MoqMyLogger()))
            {
                var address = new Address
                {
                    Street = "Testing 23",
                    City = "Warsaw",
                    Country = "Poland",
                    ZipCode = "12-678"
                };

                var result = homeController.AddToAddressDBOrGetID(address);

                Assert.Equal(6, result);
            }
        }
        
        [Fact(DisplayName = "CheckOut invalid model of ShoppingCartViewModel")]
        public void CheckOut_InvalidModelOfShoppingCartViewModel()
        {
            using (var homeController = new HomeController(MockData.MoqLogger(), MockData.MoqShopContext(),
                MockData.MoqEmailSender(), MockData.MoqMyLogger()))
            {
                var user = new User
                {
                    UserName = "Jan Nowak",
                    Email = "test@test.pl",
                    Phone = "+48 605 337 334"
                };

                var billingAddress = new Address
                {
                    Country = "Poland",
                    City = "Warsaw",
                    ZipCode = "01-111",
                    Street = "Marszałkowska 123A"
                };
                
                var scvm = new ShoppingCartViewModel()
                {
                    Order = new Order
                    {
                        UserId = 2,
                        Payment = true,
                        Draft = false,
                        Date = new DateTime(2021, 2, 27, 9, 10, 23),
                        Status = null,
                        TotalPrice = 0.00,
                        BillingAddress = billingAddress,
                        ShippingAddress = billingAddress,
                        User = user
                    },
                    Basket = MockData.GetRandomMoqOrderedBooksList().ToList(),
                    ShippingEqualBilling = true
                };

                homeController.ModelState.AddModelError(string.Empty, "Test Error");
 
                Assert.False(homeController.ModelState.IsValid, "Model state has remained valid.");
 
                // Test for BadRequest being returned
                var result = homeController.CheckOut(scvm) as ViewResult;
                var model = result?.Model;
 
                Assert.Equal("ShoppingCart", result?.ViewName);
                Assert.True(model.IsSameOrEqualTo(scvm));

            }
        }
        
        [Fact(DisplayName = "CheckOut valid model of ShoppingCartViewModel same shipping and billing address")]
        public void CheckOut_ValidModelOfShoppingCartViewModelSameAddresses()
        {
            using (var homeController = new HomeController(MockData.MoqLogger(), MockData.MoqShopContext(),
                MockData.MoqEmailSender(), MockData.MoqMyLogger()))
            {
                var user = new User
                {
                    UserName = "Jan Nowak",
                    Email = "test@test.pl",
                    Phone = "+48 605 337 334"
                };

                var billingAddress = new Address
                {
                    Country = "Poland",
                    City = "Warsaw",
                    ZipCode = "01-111",
                    Street = "Marszałkowska 123A"
                };
                
                var scvm = new ShoppingCartViewModel()
                {
                    Order = new Order
                    {
                        UserId = 2,
                        Payment = true,
                        Draft = false,
                        Date = new DateTime(2021, 2, 27, 9, 10, 23),
                        Status = null,
                        TotalPrice = 0.00,
                        BillingAddress = billingAddress,
                        ShippingAddress = billingAddress,
                        User = user
                    },
                    Basket = MockData.GetRandomMoqOrderedBooksList().ToList(),
                    ShippingEqualBilling = true
                };

                double totalPrice = 0;

                foreach (var book in scvm.Basket)
                {
                    totalPrice += book.Price * book.Quantity;
                }

                totalPrice = Math.Round(totalPrice, 2, MidpointRounding.AwayFromZero);

                var controllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() {Session = new MockHttpSession()}
                };

                homeController.ControllerContext = controllerContext;
                homeController.HttpContext.Session.Set("userId", "8befe3cc-024b-4084-ab38-8f5c7663cd1d");

              
                var action = homeController.CheckOut(scvm) as RedirectToActionResult;
              
                Assert.Equal("Payment", action?.ActionName);
                Assert.Equal("5", action?.RouteValues["orderId"]?.ToString());
                Assert.Equal(totalPrice.ToString(CultureInfo.InvariantCulture), action?.RouteValues["totalPrice"]?.ToString());
            }
        }
        
        [Fact(DisplayName = "CheckOut valid model of ShoppingCartViewModel different shipping and billing address")]
        public void CheckOut_ValidModelOfShoppingCartViewModelDifferentAddresses()
        {
            using (var homeController = new HomeController(MockData.MoqLogger(), MockData.MoqShopContext(),
                MockData.MoqEmailSender(), MockData.MoqMyLogger()))
            {
                var user = new User
                {
                    UserName = "Jan Nowak",
                    Email = "test@test.pl",
                    Phone = "+48 605 337 334"
                };

                var billingAddress = new Address
                {
                    Country = "Poland",
                    City = "Warsaw",
                    ZipCode = "01-111",
                    Street = "Marszałkowska 123A"
                };
                
                var shippingAddress = new Address
                {
                    Country = "Poland",
                    City = "Cracow",
                    ZipCode = "03-456",
                    Street = "Spacerowa 31"
                };
                
                var scvm = new ShoppingCartViewModel()
                {
                    Order = new Order
                    {
                        UserId = 2,
                        Payment = true,
                        Draft = false,
                        Date = new DateTime(2021, 2, 27, 9, 10, 23),
                        Status = null,
                        TotalPrice = 0.00,
                        BillingAddress = billingAddress,
                        ShippingAddress = shippingAddress,
                        User = user
                    },
                    Basket = MockData.GetRandomMoqOrderedBooksList().ToList(),
                    ShippingEqualBilling = false
                };

                double totalPrice = 0;

                foreach (var book in scvm.Basket)
                {
                    totalPrice += book.Price * book.Quantity;
                }

                totalPrice = Math.Round(totalPrice, 2, MidpointRounding.AwayFromZero);

                var controllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() {Session = new MockHttpSession()}
                };

                homeController.ControllerContext = controllerContext;
                homeController.HttpContext.Session.Set("userId", "8befe3cc-024b-4084-ab38-8f5c7663cd1d");

              
                var action = homeController.CheckOut(scvm) as RedirectToActionResult;
              
                Assert.Equal("Payment", action?.ActionName);
                Assert.Equal("5", action?.RouteValues["orderId"]?.ToString());
                Assert.Equal(totalPrice.ToString(CultureInfo.InvariantCulture), action?.RouteValues["totalPrice"]?.ToString());
            }
        }
    }
}