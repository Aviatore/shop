using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using shop.Models;

namespace shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ShoppingCart()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CheckOut(int orderId)
        {
            Checkout checkout = new Checkout();
            checkout.OrderId = orderId;
            return View(checkout);
        }

        [HttpPost]
        public IActionResult CheckOut(Checkout checkout)
        {
            if (ModelState.IsValid) { //checking model state
                //add checkout to db 
                
                return RedirectToAction("Payment");
            }
            return View("CheckOut");
        }

        public IActionResult Payment()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}