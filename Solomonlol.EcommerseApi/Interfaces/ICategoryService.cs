using Solomonlol.EcommerseApi.Models.Dto;
using Solomonlol.EcommerseApi.Models.Dto.Category;
using Solomonlol.EcommerseApi.MyResults;
using Solomonlol.EcommerseApi.Services.Extensions.Filters;
using Solomonlol.EcommerseApi.Services.Extensions.Sort;

namespace Solomonlol.EcommerseApi.Interfaces
{
    public interface ICategoryService
    {
        Task<Result> Create(CategoryDto item, CancellationToken ct = default);
        Task<Result> Update(string name, CategoryDto item, CancellationToken ct = default);
        Task<Result> Delete(string name, CancellationToken ct = default);
        Task<Result<CategoryDto>> Get(string name, CancellationToken ct = default);
        Task<Result<PagedResult<CategoryDto>>> GetAll(CategoryFilter filter, SortParams sortParams, int page = 1, int pageSize = 5, CancellationToken ct = default);

    }
}
