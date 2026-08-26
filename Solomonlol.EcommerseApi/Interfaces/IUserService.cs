using Solomonlol.EcommerseApi.Models.Base;

namespace Solomonlol.EcommerseApi.Interfaces
{
    public interface IUserService
    {
        Task Create(CancellationToken ct = default);
        Task Delete(string login, CancellationToken ct = default);
        Task Update(User user, CancellationToken ct = default);
        Task<User> Get(CancellationToken ct=default);
    }
}
