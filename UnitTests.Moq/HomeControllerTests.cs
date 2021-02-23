using System;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using shop.Controllers;
using shop.Models;
using shop.Utilities;
using Xunit;

namespace UnitTests.Moq
{
    public class HomeControllerTests
    {
        private readonly HomeController _homeControllerMock;
        private readonly Mock<ILogger<HomeController>> _loggerMock = new Mock<ILogger<HomeController>>();
        private readonly Mock<shopContext> _shopContextMock = new Mock<shopContext>();
        private readonly Mock<IEmailSender> _emailSenderMock = new Mock<IEmailSender>();
        private readonly Mock<IMyLogger> _myLoggerMock = new Mock<IMyLogger>();
        
        
        public HomeControllerTests()
        {
            _homeControllerMock = new HomeController(_loggerMock.Object, _shopContextMock.Object,
                _emailSenderMock.Object, _myLoggerMock.Object);
        }

        [Fact]
        public IActionResult Index_ShouldReturnBooks_WhenBooksExists()
        {
            // Arrange

            //Act


            // Assert
        }
    }
}