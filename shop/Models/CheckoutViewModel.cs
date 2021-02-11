using System.ComponentModel.DataAnnotations;
// using shop.CustomValidation;

namespace shop.Models
{
    public class CheckoutViewModel
    {
        // public int OrderId { get; set; }
        
        [Required]
        public string Name { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Phone number")]
        [Phone]
        [Required]
        public string Phone { get; set; }
        
        
        [Display(Name = "Country")]
        [Required]
        public string BillingCountry { get; set; }
        
        [Display(Name = "City")]
        [Required]
        public string BillingCity { get; set; }
        
        [Display(Name = "Zip Code")]
        [RegularExpression(@"^\d{2,5}(?:[-\s]\d{3,4})?$")]
        [Required]
        public string BillingZip { get; set; }
        
        [Display(Name = "Address")]
        [Required]
        public string BillingAddress { get; set; }
        
        
        [Display(Name = "Shipping address is different than my billing address.")]
        [Required]
        public bool BillingDifferentThanShipping { get; set; }

        //TODO: requiredWhen
        // [RequiredWhen("BillingDifferentThanShipping", true)]
        // [Required]
        [Display(Name = "Country")]
        public string ShippingCountry { get; set; }
        
        // [Required]
        [Display(Name = "City")]
        public string ShippingCity { get; set; }
        
        // [Required]
        [Display(Name = "Zip Code")]
        [RegularExpression(@"^\d{2}(?:[-\s]\d{3})?$")]
        public string ShippingZip { get; set; }
        
        // [Required]
        [Display(Name = "Address")]
        public string ShippingAddress { get; set; }
    }
}