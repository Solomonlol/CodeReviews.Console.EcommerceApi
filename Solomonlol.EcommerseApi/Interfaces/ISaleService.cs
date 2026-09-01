using Solomonlol.EcommerseApi.Models.Base;
using Solomonlol.EcommerseApi.Models.Dto;
using Solomonlol.EcommerseApi.Models.Dto.Sale;
using Solomonlol.EcommerseApi.MyResults;

namespace Solomonlol.EcommerseApi.Interfaces
{
    public interface ISaleService
    {
        Task<Result<PagedResult<SaleDto>>> GetAll(int page=1, int pageSize = 5, CancellationToken ct = default);
        Task<Result<PagedResult<SaleDto>>> GetAllByLogin(string login, int page = 1, int pageSize = 5, CancellationToken ct = default);
        Task<Result<SaleDto>> Get(int id, CancellationToken ct = default);
        Task<Result<SaleDto>> Create(Sale sale, CancellationToken ct = default);
        Task<Result> Update(SaleDto sale, CancellationToken ct = default);
        Task<Result> Delete(int saleId, CancellationToken ct = default);
    }
}
