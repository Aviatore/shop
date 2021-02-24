using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;
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
                
                Assert.Equal("Little Brown Book Group", model.Books.Single(x => x.BookId == 11).Publisher.PublisherName);
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

        // [Theory]
        // [InlineData(1, 2)]
        // public void IndexPostTest(int id, int quantity)
        // {
        //     using (var homeController = new HomeController(MockData.MoqLogger(), MockData.MoqShopContext(), 
        //         MockData.MoqEmailSender(), MockData.MoqMyLogger()))
        //     {
        //         OrderedBook orderedBook = new OrderedBook {BookId = id, Quantity = quantity};
        //         List<OrderedBook> mockBasketList = new List<OrderedBook>();
        //         mockBasketList.Add(orderedBook);
        //         
        //         // var mockHttpAccessor = new Mock<IHttpContextAccessor>();
        //         // var httpContext = new DefaultHttpContext();
        //         // var controllerContext = new ControllerContext() { HttpContext = new DefaultHttpContext()};
        //         // var sessionCart = Guid.NewGuid().ToString();
        //         // httpContext.Request.Headers["Tenant-ID"] = sessionCart;
        //
        //         // mockHttpAccessor.Setup(_ => _.HttpContext).Returns(httpContext);
        //         // var mockHeaderConfiguration = new Mock<IHeaderConfiguration>();
        //
        //         // homeController.ControllerContext = controllerContext;
        //         
        //         // httpContext.Session.Set(sessionCart, mockBasketList);
        //         
        //         // var result = homeController.AddBooksToSessionBasket(id, quantity);
        //         
        //         // Assert.Equal(1, result.Count(book => book.BookId == 1));
        //
        //         // var sessionCart = Guid.NewGuid().ToString();
        //         // var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        //         // var httpContext = new DefaultHttpContext();
        //         // httpContext.Request.Headers["token"] = sessionCart; //Set header
        //         // var controllerContext = new ControllerContext() {
        //         //     HttpContext = httpContext,
        //         // };
        //         //
        //         // mockHttpContextAccessor.Setup(x => x.HttpContext)
        //         //     .Returns(httpContext);
        //         //
        //         // homeController.ControllerContext = controllerContext;
        //         
        //         // httpContext.Session.Set(sessionCart, mockBasketList);
        //         
        //         var request = new Mock<HttpRequest>();
        //         request.Setup(x => x.Scheme).Returns("http");
        //         request.Setup(x => x.Host).Returns(HostString.FromUriComponent("http://localhost:8080"));
        //         request.Setup(x => x.PathBase).Returns(PathString.FromUriComponent("/UnitTests"));
        //
        //         var httpContext = Mock.Of<HttpContext>(_ => 
        //             _.Request == request.Object
        //         );
        //         
        //         var controllerContext = new ControllerContext { HttpContext = httpContext};
        //         homeController.ControllerContext = controllerContext;
        //
        //         var result = homeController.IndexPost(id, quantity);
        //
        //     }
        // }
    }
}