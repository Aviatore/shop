using System.Collections.Generic;

namespace shop.Models
{
    public class BookGenre
    {
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    }
}