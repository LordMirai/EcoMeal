using EcoMeal.Entities;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcoMeal.Database
{
    public class EcoMealDbContext : IdentityDbContext<ApplicationUser>
    {
        public static EcoMealDbContext? context;

        public EcoMealDbContext(DbContextOptions<EcoMealDbContext> options) : base(options)
        {
            context = this;
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<BusinessType> BusinessTypes { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<PackageType> PackageTypes { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<OrderEntry> OrderEntries { get; set; }

    }
}
