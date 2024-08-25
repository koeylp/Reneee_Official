using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Reneee.Domain.Entities;
using Reneee.Persistence;
using Attribute = Reneee.Domain.Entities.Attribute;

namespace Reneee.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Attribute> Attributes { get; set; }
        public DbSet<AttributeValue> AttributeValues { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductPromotion> ProductPromotions { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ResetPassword> ResetPasswords { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<District> Districts { get; set; } 
        public DbSet<Ward> Wards { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Gender)
                      .HasConversion<string>();

                entity.Property(e => e.Role)
                      .HasConversion<string>();

                entity.HasIndex(e => e.Email)
                      .IsUnique();
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Total)
                    .HasPrecision(18, 2);
            });

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.Property(e => e.Price)
                    .HasPrecision(18, 2);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.DiscountPrice)
                    .HasPrecision(18, 2);

                entity.Property(e => e.OriginalPrice)
                    .HasPrecision(18, 2);

            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.Total)
                    .HasPrecision(18, 2);
                entity.HasOne(e => e.Order)
                    .WithMany()
                    .HasForeignKey(e => e.OrderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProductAttribute>(entity =>
            {
                entity.Property(e => e.AttributePrice)
                    .HasPrecision(18, 2);
                entity.Property(e => e.AttributeDiscountPrice)
                    .HasPrecision(18, 2);
            });

            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.Property(e => e.DiscountValue)
                    .HasPrecision(18, 2);

            });

            modelBuilder.Entity<Sales>(entity =>
            {
                entity.Property(e => e.TotalSales)
                .HasPrecision(18, 2);
                //entity<Sales
            });
        }
    }
}
