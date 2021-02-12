using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using shop.Models;
using shop.Utility;

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
        
        [HttpPost, ActionName("Index")]
        public IActionResult IndexPost(int id, int quantity)
        {
            var orderedBooks = AddBooksToSessionBasket(id, quantity);
            HttpContext.Session.Set(WebConst.SessionCart, orderedBooks);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult BookDetails(int bookId)
        {
            var booksListById = _dbContext.Books.Include(p => p.Publisher).Include(g => g.Genre).FirstOrDefault(bId => bId.BookId == bookId);

            return View(booksListById);
        }
        
        [HttpPost, ActionName("BookDetails")]
        public IActionResult BookDetailsPost(int id, int quantity)
        {
            var orderedBooks = AddBooksToSessionBasket(id, quantity);
            HttpContext.Session.Set(WebConst.SessionCart, orderedBooks);

            return RedirectToAction(nameof(BookDetails), new {bookId = id});
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
        
        [HttpPost, ActionName("BooksByGenre")]
        public IActionResult BooksByGenrePost(int id, int quantity, int gId)
        {
            var orderedBooks = AddBooksToSessionBasket(id, quantity);
            HttpContext.Session.Set(WebConst.SessionCart, orderedBooks);

            return RedirectToAction(nameof(BooksByGenre), new {genreId = gId});
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
        
        
        [HttpPost, ActionName("BooksByPublisher")]
        public IActionResult BooksByPublisherPost(int id, int quantity, int pId)
        {
            var orderedBooks = AddBooksToSessionBasket(id, quantity);
            HttpContext.Session.Set(WebConst.SessionCart, orderedBooks);

            return RedirectToAction(nameof(BooksByPublisher), new {publisherId = pId});
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ShoppingCart()
        {
            List<BookQuantity> booksInBasket = new List<BookQuantity>();
            // foreach (var orderedBook in cookies) // orderedBook.BookId & orderedBook.Quantity
            // {
            //     int id = orderedBook.BookId;
            //     int quantity = orderedBook.Quantity;
            //     BookQuantity bookQuantity = new BookQuantity(_dbContext.Books.FirstOrDefault(b => b.BookId == id), quantity);
            //     booksInBasket.Add(bookQuantity);
            // }
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

        // public IActionResult OrderConfirmation(bool success)
        // {
        //     // TODO: error or order details => order in JSON, email
        //     return View();
        // }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
        
        public IEnumerable<OrderedBook> AddBooksToSessionBasket(int id, int quantity)
        {
            List<OrderedBook> orderedBooks = new List<OrderedBook>();
            if (HttpContext.Session.Get<IEnumerable<OrderedBook>>(WebConst.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<OrderedBook>>(WebConst.SessionCart).Any())
            {
                orderedBooks = HttpContext.Session.Get<List<OrderedBook>>(WebConst.SessionCart);
            }

            
            if (orderedBooks.Count > 0)
            {
                bool isAdd = false;
                foreach (var book in orderedBooks)
                {
                    if (book.BookId == id)
                    {
                        var sum = book.Quantity + quantity;
                        book.Quantity = sum;
                        isAdd = true;
                        break;
                    }
                }

                if (!isAdd)
                {
                    orderedBooks.Add(new OrderedBook {BookId = id, Quantity = quantity});
                }
                else
                {
                    isAdd = false;
                }
                
            }
            else
            {
                orderedBooks.Add(new OrderedBook {BookId = id, Quantity = quantity});
            }

            return orderedBooks;
        }
    }
}