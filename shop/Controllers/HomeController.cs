using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        
        public IActionResult BooksByGenre(int genreId)
        {
            IEnumerable<Book> booksListByGenre = _dbContext.Books.Include(p => p.Publisher).Include(g => g.Genre).Where(gId => gId.GenreId == genreId);
            ViewData["genreName"] = _dbContext.Genres.FirstOrDefault(g => g.GenreId == genreId)?.GenreName;
            
            return View(booksListByGenre);
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
            List<BookQuantity> booksInBasket = new List<BookQuantity>();
            foreach (var orderedBook in cookies) // orderedBook.BookId & orderedBook.Quantity
            {
                int id = orderedBook.BookId;
                int quantity = orderedBook.Quantity;
                BookQuantity bookQuantity = new BookQuantity(_dbContext.Books.FirstOrDefault(b => b.BookId == id), quantity);
                booksInBasket.Add(bookQuantity);
            }
            ShoppingCartViewModel shoppingCartVM = new ShoppingCartViewModel(booksInBasket);
            return View(shoppingCartVM);
        }
        
        [HttpPost]
        public IActionResult CheckOut(ShoppingCartViewModel scvm)
        {
            if (ModelState.IsValid) {
                //TODO: add full order to db 
                // _dbContext.Orders.Add(order);

                // TODO: how to get this order id to pass to Payment?
                return RedirectToAction("Payment", new
                {
                    orderId = scvm.Order.OrderId, 
                    totalPrice = scvm.TotalPrice()
                });
            }
            return View("ShoppingCart");
        }

        public IActionResult Payment(int orderId, double totalPrice)
        {
            PaymentViewModel paymentVM = new PaymentViewModel(orderId, totalPrice);
            return View(paymentVM);
        }
        
        
        [HttpPost]
        public IActionResult PaymentPost(PaymentViewModel pvm)
        {
            if (ModelState.IsValid) {
                //TODO: update order.payment = true
                // _dbContext.Orders.Add(order);

                //TODO: redirect to confirmation site
                return RedirectToAction("OrderConfirmation", true);
            }
            
            // TODO: info, że failed, przekaż parametry do payment
            return RedirectToAction("OrderConfirmation", false);
        }

        public IActionResult OrderConfirmation(bool success)
        {
            // TODO: error or order details => order in JSON, email
            
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}