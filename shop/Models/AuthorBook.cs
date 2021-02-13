using System;
using System.Collections.Generic;

#nullable disable

namespace shop.Models
{
    public partial class AuthorBook
    {
        public int AuthorBookId { get; set; }
        public int BookId { get; set; }
        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }
        public virtual Book Book { get; set; }
    }
}
