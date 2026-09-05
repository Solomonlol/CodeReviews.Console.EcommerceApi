using Microsoft.AspNetCore.Identity;
using Solomonlol.EcommerseApi.Interfaces;
using Solomonlol.EcommerseApi.Models.Base;

namespace Solomonlol.EcommerseApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly IPasswordHasher<User> _hasher;
        public AccountService(IPasswordHasher<User> hasher)
        {
            _hasher = hasher;
        }
        public string Hash(User user, string password, CancellationToken ct = default)
        {
            return _hasher.HashPassword(user, password);
        }

        public bool CheckHash(User user, string password, CancellationToken ct = default)
        {
            return _hasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Success ? true : false;
        }

    }
}
