using API.Common.Domain.Balance;
using API.Common.Domain.Logs;
using API.Common.Domain.Orders;
using API.Common.Domain.Product;
using API.Common.Domain.ProductImage;
using API.Common.Domain.Roles;
using API.Common.Domain.SystemAdmin;
using API.Common.Domain.Users;
using API.Common.Persistence.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations.UserBalance;

namespace Persistence.Context
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModelConfigurations.ApplyAllConfigurations(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserBalanceConfiguration());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<UserBalance> UserBalances { get; set; }
        public DbSet<ProductImage > ProductImages { get; set; } 
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<SystemAdmin> SystemAdmins { get; set; }
        public DbSet<WatchDogLogModel> WatchDog_Logs { get; set; }
    }
}