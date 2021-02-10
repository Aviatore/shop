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
            OrderViewModel order = new OrderViewModel();
            order.OrderId = 1;
            return View(order);
        }
        
        [HttpPost]
        public IActionResult CheckOut(OrderViewModel order)
        {
            var orderdd = order;
            if (ModelState.IsValid) { //checking model state
                    
                //TODO: add full order to db 
                
                return RedirectToAction("Payment", orderdd);
            }
            return View("ShoppingCart");
        }

        public IActionResult Payment(OrderViewModel order)
        {
            var orderss = order;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}