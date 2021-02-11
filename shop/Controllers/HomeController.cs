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
            BookGenrePublisher bookGenrePublisher = new BookGenrePublisher();
            IEnumerable<Publisher> publishersList = _dbContext.Publishers;
            IEnumerable<Genre> genresList = _dbContext.Genres;
            IEnumerable<Book> booksList = _dbContext.Books.Include(g => g.Genre).Include(p => p.Publisher);

            bookGenrePublisher.Publishers = publishersList;
            bookGenrePublisher.Genres = genresList;
            bookGenrePublisher.Books = booksList;

            return View(bookGenrePublisher);
        }

        public IActionResult BookDetails(int bookId)
        {
            var booksListById = _dbContext.Books.Include(p => p.Publisher).Include(g => g.Genre).FirstOrDefault(bId => bId.BookId == bookId);

            return View(booksListById);
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