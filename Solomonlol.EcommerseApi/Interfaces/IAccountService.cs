using Microsoft.AspNetCore.Identity;
using Solomonlol.EcommerseApi.Models.Base;

namespace Solomonlol.EcommerseApi.Interfaces
{
    public interface IAccountService
    {
        string Hash(User user, string password, CancellationToken ct = default);
        bool CheckHash(User user, string password, CancellationToken ct = default);
    }
}
