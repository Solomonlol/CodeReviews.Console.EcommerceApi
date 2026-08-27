using Solomonlol.EcommerseApi.Models.Base;

namespace Solomonlol.EcommerseApi.Interfaces
{
    public interface ICategoryService
    {
        Task Create(Category item, CancellationToken ct = default);
        Task Update(string name, Category item, CancellationToken ct = default);
        Task Delete(string name, CancellationToken ct = default);
        Task<Category?> Get(string name, CancellationToken ct = default);
        Task<IEnumerable<Category>?> GetAll(CancellationToken ct = default);
    }
}
