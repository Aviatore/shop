namespace shop.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        
        public ShoppingCartViewModel ShoppingCartViewModel { get; set; }
        public CheckoutViewModel CheckoutViewModel { get; set; }
        public PaymentViewModel PaymentViewModel { get; set; }
    }
}