using Salon.Infrastructure.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Salon.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string Office { get; set; }

        public int Salary { get; set; }
        public string Image { get; set; } = "noimage.png";

        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpload { get; set; }
    }
}
