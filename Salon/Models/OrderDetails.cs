using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Salon.Models
{
    [Table("OrderDetails")]
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int OrderModelId { get; set; }
        [ForeignKey("OrderModelId")]
        public OrderModel OrderModel { get; set; }
        [Required]
        public long ProcedureId { get; set; }
        [ForeignKey("ProcedureId")]
        public Procedure Procedure { get; set; }
        [Required]
        public string ProcedureName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
