using EcommerseAPI.Frontend.Entities.Dto;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerseAPI.Frontend.Interfaces
{
    internal interface ITableDrawingService
    {
        Task DrowSimpleTable<T>(PagedResult<T>? pagedResult = null,  string? title = null, CancellationToken ct = default, IEnumerable<T>? enumerableValues = null);
    }
}
