﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
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
            List<OrderedBook> orderedBooks = new List<OrderedBook>(); // cookies
            List<OrderedBook> booksInBasket = new List<OrderedBook>(); //shopping cart
            
            if (HttpContext.Session.Get<IEnumerable<OrderedBook>>(WebConst.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<OrderedBook>>(WebConst.SessionCart).Any())
            {
                orderedBooks = HttpContext.Session.Get<List<OrderedBook>>(WebConst.SessionCart);
            }
            
            // foreach (var orderedBook in orderedBooks)
            // {
            //     int id = orderedBook.BookId;
            //     int quantity = orderedBook.Quantity;
            //     BookQuantity bookQuantity = new BookQuantity(_dbContext.Books.FirstOrDefault(b => b.BookId == id), quantity);
            //     booksInBasket.Add(bookQuantity);
            // }
            
            foreach (var orderedBook in orderedBooks)
            {
                Book book = _dbContext.Books.FirstOrDefault(b => b.BookId == orderedBook.BookId);
                orderedBook.Title = book.Title;
                orderedBook.Price = book.Price;
            }
            
            
            ShoppingCartViewModel shoppingCartVM = new ShoppingCartViewModel();
            shoppingCartVM.Basket = orderedBooks;
            return View(shoppingCartVM);
        }

        [HttpPost]
        public IActionResult CheckOut(ShoppingCartViewModel scvm)
        {
            if (ModelState.IsValid)
            {
                var billAdd = scvm.Order.BillingAddress;
                _dbContext.Addresses.Add(billAdd);
                _dbContext.SaveChanges();
                _dbContext.Entry(billAdd).GetDatabaseValues();
                scvm.Order.BillingAddressId = billAdd.AddressId;
                    
                var shipAdd = scvm.Order.ShippingAddress;
                _dbContext.Addresses.Add(shipAdd);
                _dbContext.SaveChanges();
                _dbContext.Entry(shipAdd).GetDatabaseValues();
                scvm.Order.ShippingAddressId = shipAdd.AddressId;
                
                var user = scvm.Order.User;
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                _dbContext.Entry(user).GetDatabaseValues();
                scvm.Order.UserId = user.UserId;
                
                var order = scvm.Order;
                _dbContext.Orders.Add(order);
                _dbContext.SaveChanges();
                _dbContext.Entry(order).GetDatabaseValues();
                int orderId = order.OrderId;

                foreach (var orderedBook in scvm.Basket)
                {
                    var book = new BooksOrdered();
                    book.BookId = orderedBook.BookId;
                    book.OrderId = orderId;
                    for (var i = 0; i < orderedBook.Quantity; i++)
                    {
                        _dbContext.BooksOrdereds.Add(book);
                    }
                    _dbContext.SaveChanges();
                }
                

                // TODO: pass order id to Payment
                return RedirectToAction("Payment", new
                {
                    totalPrice = scvm.TotalPrice()
                });
            }
            return View("ShoppingCart");
        }

        public IActionResult Payment(int orderId, double totalPrice)
        {
            PaymentViewModel paymentVM = new PaymentViewModel();
            paymentVM.TotalPrice = totalPrice;
            paymentVM.OrderId = orderId;
            return View(paymentVM);
        }
        
        
        [HttpPost]
        public IActionResult PaymentPost(PaymentViewModel pvm)
        {
            return null;
            // if (ModelState.IsValid) {
            //     //TODO: update order.payment = true
            //     // _dbContext.Orders.Add(order);
            //
            //     //TODO: redirect to confirmation site
            //     return RedirectToAction("OrderConfirmation", true);
            // }
            //
            // // TODO: info, że failed, przekaż parametry do payment
            // return RedirectToAction("OrderConfirmation", false);
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