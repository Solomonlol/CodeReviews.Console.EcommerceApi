using EcommerseAPI.Frontend.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerseAPI.Frontend.Interfaces
{
    internal interface ILoginService
    {
        Task LogIn(LoginRequest request, CancellationToken ct = default);
        Task LogOut(CancellationToken ct = default);
    }
}
