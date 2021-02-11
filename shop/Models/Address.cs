using System;
using System.Collections.Generic;

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
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public virtual ICollection<Order> OrderBillingAddresses { get; set; }
        public virtual ICollection<Order> OrderShippingAddresses { get; set; }
    }
}
