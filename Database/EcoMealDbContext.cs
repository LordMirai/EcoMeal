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
        public DbSet<Business> Businesses { get; set; }
        public DbSet<BusinessType> BusinessTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderEntry> OrderEntries { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PackageType> PackageTypes { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletTransaction> WalletTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Business)
                .WithMany()
                .HasForeignKey("BusinessId")
                .OnDelete(DeleteBehavior.Restrict);


            // so because we're using 'decimal' for wallet stuff, EFC is complaining, so this is to shut it up.
            modelBuilder.Entity<Wallet>()
                .Property(w => w.Balance)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<WalletTransaction>()
                .Property(t => t.Amount)
                .HasColumnType("decimal(18,2)");
        }
    }
}
