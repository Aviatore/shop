namespace shop.Models
{
    public class OrderedBook
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }

        public OrderedBook(int bookId)
        {
            BookId = bookId;
            Quantity = 1;
        }
    }
}