using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
// using shop.CustomValidation;

namespace shop.Models
{
    public class ShoppingCartViewModel
    {
        [Required]
        public Order Order { get; set; }
        
        [Required]
        public List<OrderedBook> Basket { get; set; }
        
        [Display(Name = "Check this box if Billing Address and Shipping Address are the same.")]
        [Required]
        public bool ShippingEqualBilling { get; set; }
        
        // public void UpdateBook(int bookId, int value)
        // {
        //     foreach (var book in Basket)
        //     {
        //         if (bookId == book.BookId)
        //         {
        //             book.Quantity += value;
        //             return;
        //         }
        //     }
        // }
        //
        // public void RemoveBook(int bookId)
        // {
        //     foreach (var book in Basket)
        //     {
        //         if (bookId == book.BookId)
        //         {
        //             Basket.Remove(book);
        //         }
        //     }
        // }

        public double TotalPrice()
        {
            double totalPrice = 0; 
            foreach (var orderedBook in Basket)
            {
                totalPrice += orderedBook.Price * orderedBook.Quantity;
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