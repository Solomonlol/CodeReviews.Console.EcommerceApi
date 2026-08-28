using Microsoft.EntityFrameworkCore;
using Solomonlol.EcommerseApi.Models.Base;

namespace Solomonlol.EcommerseApi.Seeding
{
    public static class SeedDb
    {
        public static async Task SeedAll(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            await db.Database.MigrateAsync();
            await SeedCategory(db);
        }

        private static async Task SeedCategory(ApplicationContext db)
        {
            if(!db.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new() { Name="CPU", Description="Category for personal computer processors"},
                    new() { Name="GPU", Description="Category for personal computer discrete vodeoadapters"},
                    new() { Name="Cases", Description="Category for personal computer cases"},
                    new() { Name="RAM", Description="Category for personal computer memory modules"},
                    new() { Name="Monitors", Description="Category for monitors"},
                    new() { Name="Headphones", Description="Category for headphones"},
                    new() { Name="Mothreboards", Description="Category for personal computer motherboardes"},
                    new() { Name="Fans", Description="Category for personal computer cooling systems"},
                    new() { Name="Mouse", Description="Category for personal computer mouses"},
                    new() { Name="Keyboardes", Description="Category for personal computer keyboardes"},
                    new() { Name="Laptops", Description="Category for laptops"},
                    new() { Name="SSD", Description="Category for SSD"},
                    new() { Name="HDD", Description="Category for HDD"},
                };

                db.Categories.AddRange(categories);
                await db.SaveChangesAsync();
            }
        }
    }
}
