using System;
using System.Collections.Generic;

#nullable disable

namespace shop.Models
{
    public partial class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public int? GenreId { get; set; }
        public string Author { get; set; }
        public int? PublisherId { get; set; }
        public double Price { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual Publisher Publisher { get; set; }
    }
}
