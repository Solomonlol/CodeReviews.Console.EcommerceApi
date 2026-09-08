using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerseAPI.Frontend.Interfaces
{
    internal interface ITokenService
    {
        Task<string> GetToken(CancellationToken ct = default);
        Task SaveToken(string token, CancellationToken ct = default);
    }
}
