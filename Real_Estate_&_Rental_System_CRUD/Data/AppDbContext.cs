using Microsoft.EntityFrameworkCore;
using Real_Estate___Rental_System_CRUD.Models;

namespace Real_Estate___Rental_System_CRUD.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Real_Estate___Rental_System_CRUD.Models.PropertyType> PropertyType { get; set; } = default!;
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Property> Properties { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<RentalContract> RentalContracts { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<PropertyType> PropertyTypes { get; set; }

    }
}
