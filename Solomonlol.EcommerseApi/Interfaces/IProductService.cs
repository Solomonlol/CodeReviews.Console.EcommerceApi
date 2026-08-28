using Solomonlol.EcommerseApi.Models.Base;
using Solomonlol.EcommerseApi.Models.Dto;
using Solomonlol.EcommerseApi.MyResults;

namespace Solomonlol.EcommerseApi.Interfaces
{
    public interface IProductService
    {
        Task<Result> Create(ProductDto item, CancellationToken ct = default);
        Task<Result> Update(string name, ProductDto item, CancellationToken ct = default);
        Task<Result> Delete(string name, CancellationToken ct = default);
        Task<Result<ProductDto>> Get(string name, CancellationToken ct = default);
        Task<Result<PagedResult<ProductDto>>> GetAll(int page=1, int pageSize=5, CancellationToken ct = default);
    }
}
