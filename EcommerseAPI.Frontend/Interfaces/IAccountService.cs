using EcommerseAPI.Frontend.Entities.Dto.Users;

namespace EcommerseAPI.Frontend.Interfaces
{
    internal interface IAccountService : IPagedResultService
    {
        Task GetMe(CancellationToken ct = default);
        Task GetOne(string login, CancellationToken ct = default);
        Task Create(UserDtoCreation user, CancellationToken ct = default);
        Task Update(CancellationToken ct = default);
        Task Delete(string login, CancellationToken ct = default);
    }
}
