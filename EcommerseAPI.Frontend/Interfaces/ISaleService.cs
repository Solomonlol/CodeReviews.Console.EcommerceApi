using EcommerseAPI.Frontend.Entities.Dto.Categories;
using EcommerseAPI.Frontend.Entities.Dto.Sales;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerseAPI.Frontend.Interfaces
{
    internal interface ISaleService : IPagedResultService
    {
        Task GetOne(int id, CancellationToken ct = default);
        Task Create(CancellationToken ct = default);
        Task Close(int id, CancellationToken ct = default);
    }
}
