namespace Salon.Models.ViewModels
{
    public class CartViewModel
    {
        public OrderModel Order { get; set; }
        public List<CartItem> CartItems { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
