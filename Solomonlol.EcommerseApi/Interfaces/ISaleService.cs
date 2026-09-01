using Solomonlol.EcommerseApi.Models.Base;
using Solomonlol.EcommerseApi.Models.Dto;
using Solomonlol.EcommerseApi.Models.Dto.Sale;
using Solomonlol.EcommerseApi.MyResults;

namespace Solomonlol.EcommerseApi.Interfaces
{
    public interface ISaleService
    {
        Task<Result<PagedResult<SaleDtoResponce>>> GetAll(int page=1, int pageSize = 5, CancellationToken ct = default);
        Task<Result<PagedResult<SaleDtoResponce>>> GetAllByLogin(string login, int page = 1, int pageSize = 5, CancellationToken ct = default);
        Task<Result<SaleDtoResponce>> Get(int id, CancellationToken ct = default);
        Task<Result<SaleDtoResponce>> Create(SaleDtoRequest sale, CancellationToken ct = default);
        Task<Result> Update(SaleDtoRequest sale, CancellationToken ct = default);
        Task<Result> Delete(int saleId, CancellationToken ct = default);
    }
}
