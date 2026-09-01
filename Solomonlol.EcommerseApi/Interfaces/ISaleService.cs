using Solomonlol.EcommerseApi.Models.Base;
using Solomonlol.EcommerseApi.Models.Dto;
using Solomonlol.EcommerseApi.MyResults;

namespace Solomonlol.EcommerseApi.Interfaces
{
    public interface ISaleService
    {
        Task<Result<PagedResult<Sale>>> GetAll(int page=1, int pageSize = 5, CancellationToken ct = default);
        Task<Result<PagedResult<Sale>>> GetAllByLogin(string login, int page = 1, int pageSize = 5, CancellationToken ct = default);
        Task<Result<Sale>> Get(int id, CancellationToken ct = default);
        Task<Result<Sale>> Create(Sale sale, CancellationToken ct = default);
        Task<Result> Update(Sale sale, CancellationToken ct = default);
        Task<Result> Delete(int saleId, CancellationToken ct = default);
    }
}
