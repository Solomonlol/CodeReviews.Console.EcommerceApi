using Microsoft.EntityFrameworkCore;
using Solomonlol.EcommerseApi.Interfaces;
using Solomonlol.EcommerseApi.Models.Base;

namespace Solomonlol.EcommerseApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationContext _db;
        public CategoryService(ApplicationContext db)
        {
            _db = db;
        }

        public async Task Create(Category item, CancellationToken ct = default)
        {
            await _db.Categories.AddAsync(item, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task Delete(string name, CancellationToken ct = default)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(c => c.Name == name, ct);
            if (category != null)
                _db.Remove(category);
            await _db.SaveChangesAsync(ct);
        }

        public async Task<Category?> Get(string name, CancellationToken ct = default)
        {
            return await _db.Categories.FirstOrDefaultAsync(c=>c.Name==name, ct);
        }

        public async Task<IEnumerable<Category>?> GetAll(CancellationToken ct = default)
        {
            return await _db.Categories.ToListAsync(ct);
        }

        public async Task Update(string name, Category item, CancellationToken ct = default)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(c => c.Name == name, ct);
            if (category != null)
                _db.Categories.Update(category);
            await _db.SaveChangesAsync(ct);
        }
    }
}
