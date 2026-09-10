using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerseAPI.Frontend.Interfaces
{
    internal interface ITableDrowingService
    {
        Task DrowSimpleTable<T>(IEnumerable<T> itemsList, string? title = null, CancellationToken ct = default);
    }
}
