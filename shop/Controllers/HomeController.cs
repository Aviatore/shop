﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using shop.Data;
using shop.Models;
using shop.Utilities;
using shop.Utility;

namespace shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly shopContext _dbContext;
        private readonly IEmailSender _emailSender;
        private readonly IMyLogger _myLogger;

        public HomeController(ILogger<HomeController> logger, shopContext dbContext, IEmailSender emailSender, IMyLogger myLogger)
        {
            _logger = logger;
            _dbContext = dbContext;
            _emailSender = emailSender;
            _myLogger = myLogger;
        }
        
        public IActionResult Index()
        {
            BookGenrePublisher bookGenrePublisher = new BookGenrePublisher();
            IEnumerable<Publisher> publishersList = _dbContext.Publishers;
            IEnumerable<Genre> genresList = _dbContext.Genres;
            IEnumerable<Book> booksList = _dbContext.Books.Include(g => g.Genre).Include(p => p.Publisher);

            bookGenrePublisher.Publishers = publishersList;
            bookGenrePublisher.Genres = genresList;
            bookGenrePublisher.Books = booksList;

            return View(bookGenrePublisher);
        }
        
        [HttpPost, ActionName("Index")]
        public IActionResult IndexPost(int id, int quantity)
        {
            var orderedBooks = AddBooksToSessionBasket(id, quantity);
            HttpContext.Session.Set(WebConst.SessionCart, orderedBooks);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult BookDetails(int bookId)
        {
            var booksListById = _dbContext.Books.Include(p => p.Publisher).Include(g => g.Genre).FirstOrDefault(bId => bId.BookId == bookId);

            return View(booksListById);
        }
        
        [HttpPost, ActionName("BookDetails")]
        public IActionResult BookDetailsPost(int id, int quantity)
        {
            var orderedBooks = AddBooksToSessionBasket(id, quantity);
            HttpContext.Session.Set(WebConst.SessionCart, orderedBooks);

            return RedirectToAction(nameof(BookDetails), new {bookId = id});
        }
        
        [HttpPost]  
        public IActionResult RedirectToBooksByGenre()  
        {
            return RedirectToAction("BooksByGenre", "Home", new {genreId = HttpContext.Request.Form["Genres"]});
        }  
        
        public IActionResult BooksByGenre(int genreId)
        {
            IEnumerable<Book> booksListByGenre = _dbContext.Books.Include(p => p.Publisher).Include(g => g.Genre).Where(gId => gId.GenreId == genreId);
            ViewData["genreName"] = _dbContext.Genres.FirstOrDefault(g => g.GenreId == genreId)?.GenreName;
            
            return View(booksListByGenre);
        }
        
        [HttpPost, ActionName("BooksByGenre")]
        public IActionResult BooksByGenrePost(int id, int quantity, int gId)
        {
            var orderedBooks = AddBooksToSessionBasket(id, quantity);
            HttpContext.Session.Set(WebConst.SessionCart, orderedBooks);

            return RedirectToAction(nameof(BooksByGenre), new {genreId = gId});
        }
        
        
        [HttpPost]  
        public IActionResult RedirectToBooksByPublishers()  
        {
            return RedirectToAction("BooksByPublisher", "Home", new {publisherId = HttpContext.Request.Form["Publishers"]});
        }  

        public IActionResult BooksByPublisher(int publisherId)
        {
            IEnumerable<Book> booksListByPublisher = _dbContext.Books.Include(p => p.Publisher).Include(g => g.Genre).Where(pId => pId.PublisherId == publisherId);
            ViewData["publisherName"] = _dbContext.Publishers.FirstOrDefault(p => p.PublisherId == publisherId)?.PublisherName;
            
            return View(booksListByPublisher);
        }
        
        
        [HttpPost, ActionName("BooksByPublisher")]
        public IActionResult BooksByPublisherPost(int id, int quantity, int pId)
        {
            var orderedBooks = AddBooksToSessionBasket(id, quantity);
            HttpContext.Session.Set(WebConst.SessionCart, orderedBooks);

            return RedirectToAction(nameof(BooksByPublisher), new {publisherId = pId});
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ShoppingCart()
        {
            List<OrderedBook> orderedBooks = GetListFromCookies();

            if (orderedBooks != null)
            {
                foreach (var orderedBook in orderedBooks)
                {
                    Book book = _dbContext.Books.FirstOrDefault(b => b.BookId == orderedBook.BookId);
                    orderedBook.Title = book.Title;
                    orderedBook.Price = book.Price;
                }   
                
                ShoppingCartViewModel shoppingCartVM = new ShoppingCartViewModel();
                shoppingCartVM.Basket = orderedBooks;
                
                return View(shoppingCartVM);
            }

            return RedirectToAction("Index");
        }

        public IActionResult ShoppingCartUpdate(int bookId, int value)
        {
            var orderedBooks = AddBooksToSessionBasket(bookId, value);
            HttpContext.Session.Set(WebConst.SessionCart, orderedBooks);
            return RedirectToAction("ShoppingCart");
        }

        public IActionResult SaveCart(string name)
        {
            List<OrderedBook> orderedBooks = GetListFromCookies();
            string id = Helper.GetUserIdByEmail(_dbContext, name);
            if (id != null)
            {
                Helper.SaveBasketOfLoggedInUser(_dbContext, orderedBooks, id);
            }
            
            return RedirectToAction("ShoppingCart");
        }

        [HttpPost]
        public IActionResult CheckOut(ShoppingCartViewModel scvm)
        {
            double totalPrice = scvm.TotalPrice();

            if (!ModelState.IsValid)
            {
                return View("ShoppingCart", scvm);
            }
            
            Address billAdd = scvm.Order.BillingAddress;
            scvm.Order.BillingAddressId = Helper.AddAddressToDbOrGetID(_dbContext, billAdd);

            if (scvm.ShippingEqualBilling || scvm.Order.BillingAddress == scvm.Order.ShippingAddress)
            {
                scvm.Order.ShippingAddressId = scvm.Order.BillingAddressId;
            }
            else
            {
                Address shipAdd = scvm.Order.ShippingAddress;
                scvm.Order.ShippingAddressId = Helper.AddAddressToDbOrGetID(_dbContext, shipAdd);   
            }

            User user = scvm.Order.User;

            /*int? userId = _dbContext.Users
                .Where(u => u.UserName.Equals(user.UserName) && u.Email.Equals(user.Email) && u.Phone.Equals(user.Phone))
                .Select(u => (int?)u.UserId)
                .FirstOrDefault();*/

            var userAuthId = HttpContext.Session.Get<string>("userId");
            scvm.Order.UserId = Helper.GetUserIdById(_dbContext, userAuthId);
            
            /*if (userId.HasValue)
                //scvm.Order.UserId = userId.Value;
            {
                var userAuthId = HttpContext.Session.Get<string>("userId");
                scvm.Order.UserId = Helper.GetUserIdById(_dbContext, userAuthId);
            }
            else
            {
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                _dbContext.Entry(user).GetDatabaseValues();
                scvm.Order.UserId = user.UserId;
            }*/
            
            (int orderId, bool updated) = Helper.AddOrderToDbOrGetId(_dbContext, scvm.Order);
            
            _myLogger.Add(orderId, "Order data accepted");

            if (updated)
            {
                Helper.UpdateOrderedBooksInDb(_dbContext, orderId, scvm.Basket);
            }
            else
            {
                Helper.AddOrderedBooksToDb(_dbContext, orderId, scvm.Basket);
            }

            return RedirectToAction("Payment", new
            {
                orderId = orderId,
                totalPrice = scvm.TotalPrice()
            });
        }

        public int AddToAddressDBOrGetID(Address data)
        {
            int? addressId = _dbContext.Addresses
                .Where(a => a.Country.Equals(data.Country) && a.City.Equals(data.City) && a.ZipCode.Equals(data.ZipCode) && a.Street.Equals(data.Street))
                .Select(a => (int?)a.AddressId)
                .FirstOrDefault();

            if (addressId.HasValue)
                return addressId.Value;
            else
            {
                _dbContext.Addresses.Add(data);
                _dbContext.SaveChanges();
                _dbContext.Entry(data).GetDatabaseValues();
                return data.AddressId;
            }
        }
        
        public IActionResult Payment(int orderId, double totalPrice)
        {
            _myLogger.Add(orderId, "Start payment");
            PaymentViewModel paymentVM = new PaymentViewModel();
            paymentVM.TotalPrice = totalPrice;
            paymentVM.OrderId = orderId;
            return View(paymentVM);
        }


        [HttpPost]
        public IActionResult PaymentPost(PaymentViewModel pvm)
        {
            if (!ModelState.IsValid)
            {
                return View("Payment", pvm);
            }

            // TO TEST ONLY!
            if (pvm.TotalPrice < 500)
            {
                pvm.SuccessfulPayment = true;
            }

            var order = _dbContext.Orders.SingleOrDefault(o => o.OrderId == pvm.OrderId);
            if (order != null)
            {
                order.Payment = pvm.SuccessfulPayment;
                _dbContext.SaveChanges();
            }

            _myLogger.Add(pvm.OrderId, "Payment accepted");
                
            return RedirectToAction("OrderConfirmation", new
            {
                success = pvm.SuccessfulPayment, 
                price = pvm.TotalPrice, 
                id = pvm.OrderId
        });
    }

        public IActionResult OrderConfirmation(bool success, double price, int id)
        {
            if (success)
            {
                var culture = CultureInfo.CreateSpecificCulture("pl-PL");
                ViewData["Message"] = $"Thank You for your order! {price.ToString("0.00", culture)} zł was successfully charged from your bank account.";
                string userId = HttpContext.Session.Get<string>("userId");
                HttpContext.Session.Clear();
                HttpContext.Session.Set("userId", userId);
                
                var order = _dbContext.Orders.Include(u => u.User).SingleOrDefault(o => o.OrderId == id);
                if (order != null)
                {
                    _emailSender.SendEmailAsync(order.User.Email, $"Order {order.OrderId.ToString()} confirmation",
                        "Your order accepted.");
                    _myLogger.Add(id, "Sent order confirmation");
                }
            }
            else
            {
                ViewData["Message"] = "We couldn't charge your account...";
            }

            //email

            return View("OrderConfirmation", (id, price, success));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        public List<OrderedBook> GetListFromCookies()
        {
            if (HttpContext.Session.Get<IEnumerable<OrderedBook>>(WebConst.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<OrderedBook>>(WebConst.SessionCart).Any())
            {
                return HttpContext.Session.Get<List<OrderedBook>>(WebConst.SessionCart);
            }

            return null;
        }
        
        public IEnumerable<OrderedBook> AddBooksToSessionBasket(int id, int quantity)
        {
            List<OrderedBook> orderedBooks = new List<OrderedBook>();
            if (HttpContext.Session.Get<IEnumerable<OrderedBook>>(WebConst.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<OrderedBook>>(WebConst.SessionCart).Any())
            {
                orderedBooks = HttpContext.Session.Get<List<OrderedBook>>(WebConst.SessionCart);
            }
            
            if (orderedBooks.Count > 0)
            {
                bool isAdd = false;
                foreach (var book in orderedBooks)
                {
                    if (book.BookId == id)
                    {
                        var sum = book.Quantity + quantity;
                        book.Quantity = sum;
                        isAdd = true;

                        if (book.Quantity <= 0)
                        {
                            orderedBooks.Remove(book);
                        }
                        break;
                    }
                }

                if (!isAdd)
                {
                    orderedBooks.Add(new OrderedBook {BookId = id, Quantity = quantity});
                }
                else
                {
                    isAdd = false;
                }

            }
            else
            {
                orderedBooks.Add(new OrderedBook {BookId = id, Quantity = quantity});
            }

            return orderedBooks;
        }
    }
}