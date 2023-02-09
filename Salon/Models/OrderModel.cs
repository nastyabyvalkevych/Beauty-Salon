using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Salon.Models
{
    [Table("OrderModel")]
    public class OrderModel
    {
        
            [Key]
            public int Id { get; set; }
            [Required]
            public string Name { get; set; }
            [Required]
          
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            [Required]
            public string Employee { get; set; }
            [Required]
            public DateTime OrderDate { get; set; }
            [Required(ErrorMessage = "Please enter an execution date")]
            public DateTime ExecutionDate { get; set; }
            [Required]
            public string PaymentType { get; set; }
            [Required]
            public string Status { get; set; }
            public string Comments { get; set; }
            public int ProcedureCount { get; set; }
    }
    
}
