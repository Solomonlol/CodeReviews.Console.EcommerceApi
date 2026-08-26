using Microsoft.EntityFrameworkCore;
using Solomonlol.EcommerseApi.Models.Base;

namespace Solomonlol.EcommerseApi
{
    public class ApplicationContext : DbContext
    {
        DbSet<Product> Products { get; set; } = null!;
        DbSet<Sale> Sales { get; set; } = null!;
        DbSet<SaleItem> SaleItems { get; set; } = null!;
        DbSet<Category> Categories { get; set; } = null!;
        DbSet<User> Users { get; set; } = null!;
        DbSet<ProductAttribute> ProductAttributes { get; set; } = null!;
        DbSet<ProductAttributeValue> ProductAttributeValues { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(c => c.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<ProductAttributeValue>()
                .HasIndex(v => new { v.ProductId, v.ProductAttributeId })
                .IsUnique();
            modelBuilder.Entity<ProductAttributeValue>()
                .HasOne(v => v.ProductAttribute)
                .WithMany(p => p.Values)
                .HasForeignKey(v => v.ProductAttributeId)
                .OnDelete(DeleteBehavior.Restrict);
                

            modelBuilder.Entity<SaleItem>()
                .HasKey(s => new { s.SaleId, s.ProductId });

            modelBuilder.Entity<Product>()
                .HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
