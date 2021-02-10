using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using shop.Data;
using shop.Models;

namespace shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly shopContext _dbContext;

        public HomeController(ILogger<HomeController> logger, shopContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            BookGenre bookGenre = new BookGenre();
            IEnumerable<Genre> genresList = _dbContext.Genres;
            IEnumerable<Book> booksList = _dbContext.Books.Include(g => g.Genre).Include(p => p.Publisher);

            bookGenre.Genres = genresList;
            bookGenre.Books = booksList;
            
            return View(bookGenre);
        }

        public IActionResult BooksByGenre(int genreId)
        {
            IEnumerable<Book> booksListByGenre = _dbContext.Books.Include(p => p.Publisher).Where(gId => gId.GenreId == genreId);
            
            return View(booksListByGenre);
        }

        public IActionResult Privacy()
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