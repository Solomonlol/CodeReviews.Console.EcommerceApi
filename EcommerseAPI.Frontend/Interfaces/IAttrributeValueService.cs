using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerseAPI.Frontend.Interfaces
{
    internal interface IAttrributeValueService
    {
        Task AddAttributeValue(CancellationToken ct = default);
        Task UpdateAttributeValue(CancellationToken ct = default);
    }
}
