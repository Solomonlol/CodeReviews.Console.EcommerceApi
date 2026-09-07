using Solomonlol.EcommerseApi.Models.Dto;
using Solomonlol.EcommerseApi.Models.Dto.User;
using Solomonlol.EcommerseApi.MyResults;
using Solomonlol.EcommerseApi.Services.Extensions.Filters;
using Solomonlol.EcommerseApi.Services.Extensions.Sort;

namespace Solomonlol.EcommerseApi.Interfaces
{
    public interface IUserService
    {
        Task<Result> Create(UserDtoCreation item, CancellationToken ct = default);
        Task<Result> Delete(string login, CancellationToken ct = default);
        Task<Result> Update(string login, UserDtoRequest item, CancellationToken ct = default);
        Task<Result<UserDtoResponse>> GetByLogin(string login, CancellationToken ct = default);
        Task<Result<UserDtoResponse>> VerifyByEmail(string email, string password, CancellationToken ct = default);
        Task<Result<PagedResult<UserDtoResponse>>> GetAll(UserFilter filter, SortParams sortParams, int page = 1, int pageSize = 5, CancellationToken ct = default);
    }
}
