using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shop.Models;
using shop.Utility;

namespace shop.Controllers
{
    public class HistoryController : Controller
    {
        private shopContext _ctx;

        public HistoryController(shopContext shopContext)
        {
            _ctx = shopContext;
        }
        // GET
        public IActionResult Index()
        {
            string userId = HttpContext.Session.Get<string>("userId");
            var user = _ctx.Users.FirstOrDefault(u => u.UserAuthId.Equals(userId));
            _ctx.Entry(user).Collection(u => u.Orders).Load();
            
            var booksOrdered = _ctx.BooksOrdereds.Where(b => b.Order)
            
            return View(user);
        }
    }
}