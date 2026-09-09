using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerseAPI.Frontend.Interfaces
{
    internal interface ITableDrowingService
    {
        Task DrowTable<T>(IEnumerable<T> itemsList, CancellationToken ct = default);
    }
}
