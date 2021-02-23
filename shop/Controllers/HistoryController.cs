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
            var user = _ctx.Users
                .Include(u => u.Orders)
                .ThenInclude(x => x.BooksOrdereds)
                .ThenInclude(x => x.Book)
                .FirstOrDefault(u => u.UserAuthId.Equals(userId));

            return View(user);
        }
    }
}