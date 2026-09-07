using Solomonlol.EcommerseApi.Models.Dto;
using Solomonlol.EcommerseApi.Models.Dto.Product;
using Solomonlol.EcommerseApi.MyResults;
using Solomonlol.EcommerseApi.Services.Extensions.Filters;
using Solomonlol.EcommerseApi.Services.Extensions.Sort;

namespace Solomonlol.EcommerseApi.Interfaces
{
    public interface IProductService
    {
        Task<Result> Create(ProductDto item, CancellationToken ct = default);
        Task<Result> Update(string name, ProductDto item, CancellationToken ct = default);
        Task<Result> Delete(string name, CancellationToken ct = default);
        Task<Result<ProductDto>> Get(string name, CancellationToken ct = default);
        Task<Result<PagedResult<ProductDto>>> GetAll(ProductFilter filter, SortParams sortParams, int page = 1, int pageSize = 5, CancellationToken ct = default);
    }
}
