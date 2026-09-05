using Solomonlol.EcommerseApi.Models.Base;
using Solomonlol.EcommerseApi.Models.Dto;
using Solomonlol.EcommerseApi.Models.Dto.Sale;
using Solomonlol.EcommerseApi.MyResults;

namespace Solomonlol.EcommerseApi.Interfaces
{
    public interface ISaleService
    {
        Task<Result<PagedResult<SaleDtoResponse>>> GetAll(int page=1, int pageSize = 5, CancellationToken ct = default);
        Task<Result<PagedResult<SaleDtoResponse>>> GetAllByLogin(string login, int page = 1, int pageSize = 5, CancellationToken ct = default);
        Task<Result<SaleDtoResponse>> Get(int id, CancellationToken ct = default);
        Task<Result<SaleDtoResponse>> Create(SaleDtoRequest sale, CancellationToken ct = default);
        //Task<Result> Update(int saleId, SaleDtoRequest sale, CancellationToken ct = default);
        Task<Result> CloseSale(int saleId, CancellationToken ct = default);
    }
}
