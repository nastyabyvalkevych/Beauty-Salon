using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Salon.Models;

namespace Salon.Infrastructure
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderModel> OrderModels { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        public DbSet<Employee> Employees { get; set; }
    }
}
