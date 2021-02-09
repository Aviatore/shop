using System;
using System.Collections.Generic;

#nullable disable

namespace shop.Models
{
    public partial class Order
    {
        public Order()
        {
            Logs = new HashSet<Log>();
        }

        public int OrderId { get; set; }
        public int BillingAddressId { get; set; }
        public int ShippingAddressId { get; set; }
        public int UserId { get; set; }
        public bool Payment { get; set; }

        public virtual Address BillingAddress { get; set; }
        public virtual Address ShippingAddress { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
    }
}
