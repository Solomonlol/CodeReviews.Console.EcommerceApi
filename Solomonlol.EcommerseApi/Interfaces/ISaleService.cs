using Solomonlol.EcommerseApi.Models.Dto;
using Solomonlol.EcommerseApi.Models.Dto.Sale;
using Solomonlol.EcommerseApi.MyResults;
using Solomonlol.EcommerseApi.Services.Extensions.Filters;
using Solomonlol.EcommerseApi.Services.Extensions.Sort;

namespace Solomonlol.EcommerseApi.Interfaces
{
    public interface ISaleService
    {
        Task<Result<PagedResult<SaleDtoResponse>>> GetAll(SaleFilter filter, SortParams sortParams, int page = 1, int pageSize = 5, CancellationToken ct = default);
        Task<Result<PagedResult<SaleDtoResponse>>> GetAllByLogin(SaleFilter filter, SortParams sortParams, string login, int page = 1, int pageSize = 5, CancellationToken ct = default);
        Task<Result<SaleDtoResponse>> Get(int id, CancellationToken ct = default);
        Task<Result<SaleDtoResponse>> Create(SaleDtoRequest sale, CancellationToken ct = default);
        Task<Result> CloseSale(int saleId, CancellationToken ct = default);
    }
}
