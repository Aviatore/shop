using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using shop.Models;

namespace shop.Data
{
    public class Helper
    {
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
        
        public static int AddAddressOrGetID(shopContext shopContext, Address data)
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
        

        public static void SaveBasketOfLoggedInUser(shopContext shopContext, List<OrderedBook> list, int userId)
        {
            //TODO!
            var order = new Order();
            order.UserId = userId;
            // TODO: order.Status = true;
            shopContext.Orders.Add(order);
            shopContext.SaveChanges();
            shopContext.Entry(order).GetDatabaseValues();
            int orderId = order.OrderId;
            
            foreach (var item in list)
            {
                var book = new BooksOrdered();
                book.BookId = item.BookId;
                book.OrderId = orderId;
                for (var i = 0; i < item.Quantity; i++)
                {
                    shopContext.BooksOrdereds.Add(book);
                }
                shopContext.SaveChanges();
            }
        }


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
    }
}