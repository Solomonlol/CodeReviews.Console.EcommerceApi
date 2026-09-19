using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerseAPI.Frontend.Interfaces
{
    internal interface IAttrributeService
    {
        Task AddAttribute(CancellationToken ct = default);
        Task UpdateAttribute(CancellationToken ct = default);
        Task DeleteAttribute(CancellationToken ct = default);
    }
}
