using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Moq;
using shop.Controllers;
using shop.Models;
using shop.Utilities;
using Xunit;

namespace UnitTests
{
    public class IndexTest
    {
        // Test makes sure that Index() method returns correct view.
        // View is correct when its name is not given or if its name is Index
        [Fact(DisplayName = "Index should return default view")]
        public void IndexShouldReturnDefaultView()
        {
            MockData data = new MockData();
            using var context = data.GetIndexShopContextTest();
            
            using (var homeController = new HomeController(MockData.MoqLogger(), context, 
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
        public void IndexShouldReturnValidModel()
        {
            MockData data = new MockData();
            using var context = data.GetIndexShopContextTest();
            
            using (var homeController = new HomeController(MockData.MoqLogger(), context, 
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
    }
}