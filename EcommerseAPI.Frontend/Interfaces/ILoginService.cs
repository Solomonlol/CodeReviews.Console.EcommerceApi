using EcommerseAPI.Frontend.Entities;

namespace EcommerseAPI.Frontend.Interfaces
{
    internal interface ILoginService
    {
        Task LogIn(LoginRequest request, CancellationToken ct = default);
        Task LogOut(CancellationToken ct = default);
    }
}
