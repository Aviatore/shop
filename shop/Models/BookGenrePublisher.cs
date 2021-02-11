using System.Collections.Generic;

namespace shop.Models
{
    public class BookGenrePublisher
    {
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<Publisher> Publishers { get; set; }
    }
}