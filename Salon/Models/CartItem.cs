using Salon.Models;

namespace Salon.Models
{
    public class CartItem
    {
        public long ProcedureId { get; set; }
        public string ProcedureName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total
        {
            get { return Quantity * Price; }
        }
        public string Image { get; set; }

        public CartItem()
        {
        }

        public CartItem(Procedure procedure)
        {
            ProcedureId = procedure.Id;
            ProcedureName = procedure.Name;
            Price = procedure.Price;
            Quantity = 1;
            Image = procedure.Image;
        }

    }
}
