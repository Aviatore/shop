using System.ComponentModel.DataAnnotations;

namespace shop.Models
{
    public class Checkout
    {
        public int OrderId { get; set; }
        
        [Required]
        public string Name { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Phone number")]
        [DataType(DataType.PhoneNumber)]
        [Required]
        public int Phone { get; set; }
        
        
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
        
        
        [Display(Name = "Country")]
        public string ShippingCountry { get; set; }
        
        [Display(Name = "City")]
        public string ShippingCity { get; set; }
        
        [Display(Name = "Zip Code")]
        [RegularExpression(@"^\d{2}(?:[-\s]\d{3})?$")]
        public string ShippingZip { get; set; }
        
        [Display(Name = "Address")]
        public string ShippingAddress { get; set; }
    }
}