using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerseAPI.Frontend.Interfaces
{
    internal interface IAttributeService
    {
        Task AddAttribute(CancellationToken ct = default);
        Task UpdateAttribute(CancellationToken ct = default);
        Task DeleteAttribute(CancellationToken ct = default);

        Task AddAttributeValue(CancellationToken ct = default);
        Task UpdateAttributeValue(CancellationToken ct = default);
    }
}
