using Solomonlol.EcommerseApi.Models.Dto;
using Solomonlol.EcommerseApi.MyResults;

namespace Solomonlol.EcommerseApi.Interfaces
{
    public interface ICategoryService
    {
        Task<Result> Create(CategoryDto item, CancellationToken ct = default);
        Task<Result> Update(string name, CategoryDto item, CancellationToken ct = default);
        Task<Result> Delete(string name, CancellationToken ct = default);
        Task<Result<CategoryDto>> Get(string name, CancellationToken ct = default);
        Task<Result<IEnumerable<CategoryDto>>> GetAll(CancellationToken ct = default);
    }
}
