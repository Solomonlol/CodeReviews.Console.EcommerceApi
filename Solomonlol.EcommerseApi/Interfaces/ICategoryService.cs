using Solomonlol.EcommerseApi.Models.Base;

namespace Solomonlol.EcommerseApi.Interfaces
{
    public interface ICategoryService
    {
        Task Create(CancellationToken ct = default);
        Task Update(Category category, CancellationToken ct = default);
        Task Delete(string name, CancellationToken ct = default);
        Task<Category> Get(string name, CancellationToken ct = default);
    }
}
