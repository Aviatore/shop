namespace shop.Models
{
    public class OrderedBook
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }


        public OrderedBook(Book book, int quantity)
        {
            BookId = book.BookId;
            Title = book.Title;
            Quantity = quantity;
            Price = book.Price;
        }

        public OrderedBook(int bookId, int quantity)
        {
            BookId = bookId;
            Quantity = quantity;
        }
        
        public OrderedBook(int bookId, int quantity, string title, double price)
        {
            BookId = bookId;
            Quantity = quantity;
            Title = title;
            Price = price;
        }

        public OrderedBook()
        {
        }
    }
}