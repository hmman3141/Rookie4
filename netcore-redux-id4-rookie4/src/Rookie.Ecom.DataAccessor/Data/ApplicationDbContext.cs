using Microsoft.EntityFrameworkCore;
using Rookie.Ecom.DataAccessor.Entities;

namespace Rookie.Ecom.DataAccessor.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductPicture> ProductPictures { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserAddress> UserAddresses { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //code first
            //db first
            //model first
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>(entity =>
            {
                entity.ToTable(name: "Category");
            });

            builder.Entity<Product>(entity =>
            {
                entity.ToTable(name: "Product");
            });

            builder.Entity<Order>(entity =>
            {
                entity.ToTable(name: "Order");
            });

            builder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable(name: "OrderDetail");
            });

            builder.Entity<ProductPicture>(entity =>
            {
                entity.ToTable(name: "ProductPicture");
            });

            builder.Entity<Rating>(entity =>
            {
                entity.ToTable(name: "Rating");
            });

            builder.Entity<User>(entity =>
            {
                entity.ToTable(name: "User");
            });

            builder.Entity<Role>(entity =>
            {
                entity.ToTable(name: "Role");
            });

            builder.Entity<UserAddress>(entity =>
            {
                entity.ToTable(name: "UserAddress");
            });

            builder.Entity<UserRole>(entity =>
            {
                entity.ToTable(name: "UserRole");
            });
        }
    }
}
