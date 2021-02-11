using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace shop.Models
{
    public partial class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public int? GenreId { get; set; }
        public string Description { get; set; }
        public string FigUrl { get; set; }
        public int? PublisherId { get; set; }
        public double Price { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual Publisher Publisher { get; set; }
    }
}