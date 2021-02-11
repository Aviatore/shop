namespace shop.Models
{
    public class OrderedBook
    {
        public int BookId { get; set; }
        public int Amount { get; set; }
        
        public double Price { get; set; }

        public OrderedBook(Book book)
        {
            BookId = book.BookId;
            Amount = 1;
            Price = book.Price;
        }
    }
}