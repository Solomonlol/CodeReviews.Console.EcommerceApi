using EcommerseAPI.Frontend.Services.Factory.Sort;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerseAPI.Frontend.Interfaces
{
    internal interface IUrlService
    {
        Task<string> GetUrl(object? filter = null, SortParams? sort = null, int? page = null, int? pageSize = null, CancellationToken ct = default);
    }
}
