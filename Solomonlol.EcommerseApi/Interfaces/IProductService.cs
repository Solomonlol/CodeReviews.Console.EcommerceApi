using Solomonlol.EcommerseApi.Models.Base;

namespace Solomonlol.EcommerseApi.Interfaces
{
    public interface IProductService
    {
        Task Create(CancellationToken ct = default);
        Task Update(Product item, CancellationToken ct = default);
        Task Delete(string name, CancellationToken ct = default);
        Task<Product> Get(string name, CancellationToken ct = default);
    }
}
