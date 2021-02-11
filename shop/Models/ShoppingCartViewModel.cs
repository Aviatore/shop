// using System;
// using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;
// // using shop.CustomValidation;
//
// namespace shop.Models
// {
//     public class ShoppingCartViewModel
//     {
//         public readonly List<OrderedBook> AnonymousBasket = new List<OrderedBook>();
//
//         public void AddToCart(Book book)
//         {
//             foreach (var orderBook in AnonymousBasket)
//             {
//                 if (book.BookId == orderBook.BookId)
//                 {
//                     orderBook.Quantity++;
//                     return;
//                 }
//             }
//
//             OrderedBook newBook = new OrderedBook(book);
//             AnonymousBasket.Add(newBook);
//         }
//
//         public void RemoveFromCart(Book book)
//         {
//             foreach (var orderBook in AnonymousBasket)
//             {
//                 if (book.BookId == orderBook.BookId)
//                 {
//                     orderBook.Quantity--;
//                     if (orderBook.Quantity == 0)
//                     {
//                         AnonymousBasket.Remove(orderBook);
//                     }
//                 }
//             }
//         }
//
//         public double TotalPrice()
//         {
//             double totalPrice = 0; 
//             foreach (var orderBook in AnonymousBasket)
//             {
//                 totalPrice += orderBook.Price * orderBook.Quantity;
//             }
//             
//             return totalPrice;
//         }
//         
//         public double TotalAmount()
//         {
//             int totalAmount = 0; 
//             foreach (var orderBook in AnonymousBasket)
//             {
//                 totalAmount += orderBook.Quantity;
//             }
//
//             return totalAmount;
//         }
//     }
// }