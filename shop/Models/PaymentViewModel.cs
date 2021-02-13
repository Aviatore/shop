using System.ComponentModel.DataAnnotations;
// using shop.CustomValidation;

namespace shop.Models
{
    public class PaymentViewModel
    {
        public bool SuccessfulPayment { get; set; }
        
        [Display(Name = "Cardholder")]
        [Required]
        public string CardName { get; set; }

        [Display(Name = "Card number")]
        [CreditCard]
        [Required]
        public string CardNumber { get; set; }

        [Display(Name = "Expiration Date")]
        [Required]
        // [RegularExpression(@"^\d{2}(?:[.\s]\d{4})?$")]
        public string CardExp { get; set; }
        
        [Display(Name = "CVV")]
        [Required]
        // [MaxLength(3)]
        // [MinLength(3)]
        public int CardCVV { get; set; }
        
        [Display(Name = "Number of Your Order")]
        [Required]
        public int OrderId { get; set; }
        
        [Display(Name = "Total Price")]
        [Required]
        public double TotalPrice { get; set; }
    }
}