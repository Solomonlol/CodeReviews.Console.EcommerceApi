using Microsoft.EntityFrameworkCore;
using Solomonlol.EcommerseApi.Models.Base;

namespace Solomonlol.EcommerseApi
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Sale> Sales { get; set; } = null!;
        public DbSet<SaleItem> SaleItems { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<ProductAttribute> ProductAttributes { get; set; } = null!;
        public DbSet<ProductAttributeValue> ProductAttributeValues { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(c => c.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Product>()
                .HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name)
                .IsUnique();

            modelBuilder.Entity<ProductAttribute>()
                .HasOne(a => a.Category)
                .WithMany(c => c.Attributes)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductAttributeValue>()
                .HasIndex(v => new { v.ProductId, v.ProductAttributeId })
                .IsUnique();
            modelBuilder.Entity<ProductAttributeValue>()
                .HasOne(v => v.ProductAttribute)
                .WithMany(p => p.Values)
                .HasForeignKey(v => v.ProductAttributeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<SaleItem>()
                .HasKey(s => new { s.SaleId, s.ProductId });

            modelBuilder.Entity<User>()
                .HasIndex(u=>u.Login)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDeleted);
        }
    }
}
