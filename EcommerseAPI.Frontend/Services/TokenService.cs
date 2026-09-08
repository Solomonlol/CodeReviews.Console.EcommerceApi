using EcommerseAPI.Frontend.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerseAPI.Frontend.Services
{
    internal class TokenService : ITokenService
    {
        
        private string _token ="";
        public Task<string> GetToken(CancellationToken ct = default)
        {
            return Task.FromResult(_token);
        }

        public Task SaveToken(string token, CancellationToken ct = default)
        {
            _token = token;
            return Task.CompletedTask;
        }
    }
}
