using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerseAPI.Frontend.Interfaces
{
    internal interface IMenu
    {
        Task StartAsync(CancellationToken ct=default);
    }
}
