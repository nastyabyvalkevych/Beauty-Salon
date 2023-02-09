using Microsoft.AspNetCore.Identity;

namespace Salon.Models
{
    public class AppUser : IdentityUser
    {
        public string Occupation { get; set; }
    }
}
