namespace shop.Models
{
    public class OrderedBook
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }


        public OrderedBook(int bookId)
        {
            BookId = book.BookId;
            Title = book.Title;
            Quantity = 1;
            Price = book.Price;
        }

        public OrderedBook(int bookId, int quantity)
        {
            BookId = bookId;
            Quantity = quantity;
        }

        public OrderedBook()
        {
        }
    }
}