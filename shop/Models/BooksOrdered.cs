using System;
using System.Collections.Generic;

#nullable disable

namespace shop.Models
{
    public partial class BooksOrdered
    {
        public int OrderId { get; set; }
        public int BookId { get; set; }

        public virtual Book Book { get; set; }
        public virtual Order Order { get; set; }
    }
}
