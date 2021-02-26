using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using shop.Controllers;
using shop.Models;
using shop.Utility;

namespace shop.Data
{
    public class Helper
    {
        public static List<T> RawSqlQuery<T>(string query, Func<DbDataReader, T> map)
        {
            using (var context = new shopContext())
            {
                using (var command = context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    context.Database.OpenConnection();

                    using (var result = command.ExecuteReader())
                    {
                        var entities = new List<T>();

                        while (result.Read())
                        {
                            entities.Add(map(result));
                        }

                        return entities;
                    }
                }
            }
            
            /*
             * Example usage:
             * using (var ctx = new shop3Context())
                {
                    var a = Helper.RawSqlQuery(
                        "select title, first_name, last_name from books_ordered left join books on books.book_id = books_ordered.book_id left join author_book on books.book_id = author_book.book_id left join authors on author_book.author_id = authors.author_id where books_ordered.order_id = 2",
                        reader => new AuthorTest()
                            {FirstName = (string) reader[0], LastName = (string) reader[1], Title = (string) reader[2]});

                    foreach (var i in a)
                    {
                        Console.WriteLine($"{i.Title} {i.FirstName} {i.LastName}");
                    }
                }
             */
        }

        public static void AddNewUser(shopContext shopContext, ApplicationDbContext applicationDbContext, string email)
        {
            var userAuth = applicationDbContext.Users.FirstOrDefault(u => u.Email.Equals(email));
            
            if (userAuth != null)
            {
                User user = new User()
                {
                    Email = email,
                    UserAuthId = userAuth.Id
                };

                shopContext.Users.Add(user);
                shopContext.SaveChanges();
            }
        }

        public static string GetUserIdByEmail(shopContext shopContext, string email)
        {
            var user = shopContext.Users.FirstOrDefault(u => u.Email.Equals(email));
        
            return user?.UserAuthId;
        }
        
        public static int GetUserIdById(shopContext shopContext, string id)
        {
            var user = shopContext.Users.First(u => u.UserAuthId.Equals(id));

            return user.UserId;
        }
        
        

        public static void SaveBasketOfLoggedInUser(shopContext shopContext, List<OrderedBook> list, string authId)
        {
            int userId = GetUserIdById(shopContext, authId);
            
            var order = new Order();
            order.UserId = userId;
            order.Draft = true;
            order.Date = DateTime.Now;
            
            //TODO: real addresses
            order.BillingAddressId = 1;
            order.ShippingAddressId = 1;
            
            shopContext.Orders.Add(order);
            shopContext.SaveChanges();
            shopContext.Entry(order).GetDatabaseValues();
            int orderId = order.OrderId;

            AddOrderedBooksToDb(shopContext, orderId, list);
        }
        
        public static int AddAddressToDbOrGetID(shopContext shopContext, Address data)
        {
            int? addressId = shopContext.Addresses
                .Where(a => a.Country.Equals(data.Country) && a.City.Equals(data.City) && a.ZipCode.Equals(data.ZipCode) && a.Street.Equals(data.Street))
                .Select(a => (int?)a.AddressId)
                .FirstOrDefault();

            if (addressId.HasValue)
                return addressId.Value;
            else
            {
                shopContext.Addresses.Add(data);
                shopContext.SaveChanges();
                shopContext.Entry(data).GetDatabaseValues();
                return data.AddressId;
            }
        }

        public static int AddOrderToDbOrGetId(shopContext shopContext, Order order)
        {
            order.User = null;
            order.BillingAddress = null;
            order.ShippingAddress = null;
            order.Date = DateTime.Now;
            shopContext.Orders.Add(order);
            shopContext.SaveChanges();
            shopContext.Entry(order).GetDatabaseValues();
            return order.OrderId;
        }

        public static void AddOrderedBooksToDb(shopContext shopContext, int orderId, List<OrderedBook> basket)
        {
            foreach (var item in basket)
            {
                IList<BooksOrdered> booksOrdereds = new List<BooksOrdered>();
                for (var i = 0; i < item.Quantity; i++)
                {
                    var book = new BooksOrdered();
                    book.BookId = item.BookId;
                    book.OrderId = orderId;
                    booksOrdereds.Add(book);
                }
                shopContext.BooksOrdered.AddRange(booksOrdereds);
                shopContext.SaveChanges();
            }
        }

        public static List<OrderedBook> GetListOfBooksInSavedShoppingCart(shopContext shopContext, string userId)
        {
            var orderId = shopContext.Orders
                .Where(o => o.User.UserAuthId == userId && o.Draft == true)
                .OrderByDescending(o => o.Date)
                .Select(o => o.OrderId)
                .FirstOrDefault();

            if (orderId != null)
            {
                List<int> bookIds = shopContext.BooksOrdered
                    .Where(b => b.OrderId == orderId)
                    .Select(b => b.BookId)
                    .ToList();

                List<OrderedBook> orderedBooks = new List<OrderedBook>();
                var hash = new HashSet<int>();

                foreach (var id in bookIds)
                {
                    int quantity = 0;
                    if (hash.Add(id))
                    {
                        quantity = bookIds
                            .Where(i => i == id)
                            .Count();
                    
                        orderedBooks.Add(new OrderedBook {BookId = id, Quantity = quantity});
                    }
                }

                return orderedBooks;
            }

            return null;
        }

        public static List<OrderedBook> ChangeQuantityOfBooks(int bookId, List<OrderedBook> orderedBooks)
        {
            if (orderedBooks.Count > 0)
            {
                bool isAdd = false;
                foreach (var book in orderedBooks)
                {
                    if (book.BookId == bookId)
                    {
                        var sum = book.Quantity++;
                        book.Quantity = sum;
                        isAdd = true;

                        if (book.Quantity <= 0)
                        {
                            orderedBooks.Remove(book);
                        }

                        break;
                    }
                }

                if (!isAdd)
                {
                    orderedBooks.Add(new OrderedBook {BookId = bookId, Quantity = 1});
                }
                else
                {
                    isAdd = false;
                }

            }
            else
            {
                orderedBooks.Add(new OrderedBook {BookId = bookId, Quantity = 1});
            }

            return orderedBooks;
        }
    }
}