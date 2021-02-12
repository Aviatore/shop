using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace shop.Models
{
    public partial class Address
    {
        public Address()
        {
            OrderBillingAddresses = new HashSet<Order>();
            OrderShippingAddresses = new HashSet<Order>();
        }

        public int AddressId { get; set; }
        
        [Display(Name = "Address")]
        [Required]
        public string Street { get; set; }
        
        [Required]
        public string City { get; set; }
        
        [Required]
        public string Country { get; set; }
        [Display(Name = "Zip Code")]
        [RegularExpression(@"^\d{2,5}(?:[-\s]\d{3,4})?$")]
        [Required]
        public string ZipCode { get; set; }

        public virtual ICollection<Order> OrderBillingAddresses { get; set; }
        public virtual ICollection<Order> OrderShippingAddresses { get; set; }
    }
}
