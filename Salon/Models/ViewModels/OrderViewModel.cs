namespace Salon.Models.ViewModels
{
    public class OrderViewModel
    {
        public OrderModel OrderModel { get; set; }
        public IEnumerable<OrderDetails> OrderDetails { get; set; }
    }
}
