using System;
using System.Collections.Generic;

#nullable disable

namespace shop.Models
{
    public partial class Publisher
    {
        public Publisher()
        {
            Books = new HashSet<Book>();
        }

        public int PublisherId { get; set; }
        public string PublisherName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}