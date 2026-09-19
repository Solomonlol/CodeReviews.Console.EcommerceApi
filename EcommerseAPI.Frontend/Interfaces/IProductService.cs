using EcommerseAPI.Frontend.Entities.Dto.Products;

namespace EcommerseAPI.Frontend.Interfaces
{
    internal interface IProductService : IPagedResultService
    {
        Task<ProductDto?> GetOne(string productName, CancellationToken ct = default);
        Task Create(ProductDto productItem, CancellationToken ct = default);
        Task Update(CancellationToken ct = default);
        Task Delete(string productName, CancellationToken ct = default);
    }
}
