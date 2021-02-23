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
    }
}