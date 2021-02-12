namespace shop.Models
{
    public class BookQuantity
    {
        public Book Book { get; set; }
        public int Quantity { get; set; }

        public BookQuantity(Book book, int quantity)
        {
            Book = book;
            Quantity = quantity;

        }
    }
}