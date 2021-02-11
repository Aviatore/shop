using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
// using shop.CustomValidation;

namespace shop.Models
{
    public class ShoppingCartViewModel
    {
        public Order Order = new Order();
        
        public readonly List<BookQuantity> Basket = new List<BookQuantity>();

        public ShoppingCartViewModel(List<BookQuantity> listOfBooks)
        {
            Basket = listOfBooks;
        }

        public void AddBook(int bookId)
        {
            foreach (var bookQuantity in Basket)
            {
                if (bookId == bookQuantity.Book.BookId)
                {
                    bookQuantity.Quantity++;
                    return;
                }
            }
            //
            // BookQuantity newBook = new BookQuantity(bookId);
            // Basket.Add(newBook);
            // TODO: uaktualnij cookies; czy uaktualnij po wyjściu z shoppingCart?
        }

        public void RemoveBook(int bookId)
        {
            foreach (var bookQuantity in Basket)
            {
                if (bookId == bookQuantity.Book.BookId)
                {
                    bookQuantity.Quantity--;
                    if (bookQuantity.Quantity == 0)
                    {
                        Basket.Remove(bookQuantity);
                    }
                }
            }
            // TODO: uaktualnij cookies; czy uaktualnij po wyjściu z shoppingCart?
        }

        public double TotalPrice()
        {
            double totalPrice = 0; 
            foreach (var bookQuantity in Basket)
            {
                totalPrice += bookQuantity.Book.Price * bookQuantity.Quantity;
            }
            
            return totalPrice;
        }
        
        public double TotalAmount()
        {
            int totalAmount = 0; 
            foreach (var bookQuantity in Basket)
            {
                totalAmount += bookQuantity.Quantity;
            }

            return totalAmount;
        }
    }
}