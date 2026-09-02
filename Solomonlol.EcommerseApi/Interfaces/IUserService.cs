using Solomonlol.EcommerseApi.Models.Base;
using Solomonlol.EcommerseApi.Models.Dto;
using Solomonlol.EcommerseApi.Models.Dto.User;
using Solomonlol.EcommerseApi.MyResults;

namespace Solomonlol.EcommerseApi.Interfaces
{
    public interface IUserService
    {
        Task<Result> Create(UserDtoCreation item, CancellationToken ct = default);
        Task<Result> Delete(string login, string password, CancellationToken ct = default);
        Task<Result> Update(string login, string password, UserDtoRequest item, CancellationToken ct = default);
        Task<Result<UserDtoRequest>> Get(string login, CancellationToken ct=default);
        Task<Result<PagedResult<UserDtoRequest>>> GetAll(int page = 1, int pageSize = 5, CancellationToken ct = default);
    }
}
