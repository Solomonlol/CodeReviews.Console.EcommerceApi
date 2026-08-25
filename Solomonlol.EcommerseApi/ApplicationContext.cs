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

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
