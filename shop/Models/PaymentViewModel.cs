using System.ComponentModel.DataAnnotations;
// using shop.CustomValidation;

namespace shop.Models
{
    public class PaymentViewModel
    {
        public bool SuccessfulPayment { get; set; }
        
        [Required]
        public string CardName { get; set; }

        [CreditCard]
        [Required]
        public string CardNumber { get; set; }

        [Display(Name = "Expiration Date")]
        [Required]
        public DataType CardExp { get; set; }
        
        [Display(Name = "CVV")]
        [Required]
        [Range(3,3)]
        public int CardCVV { get; set; }
        
        [Required]
        public int OrderId { get; set; }
        
        [Required]
        public double TotalPrice { get; set; }
    }
}