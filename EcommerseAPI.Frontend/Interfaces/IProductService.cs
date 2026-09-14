using EcommerseAPI.Frontend.Entities.Dto.Products;
using EcommerseAPI.Frontend.Entities.Dto.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerseAPI.Frontend.Interfaces
{
    internal interface IProductService : IPagedResultService
    {
        Task GetOne(string productName, CancellationToken ct = default);
        Task Create(ProductDto productItem, CancellationToken ct = default);
        Task Update(ProductDto productItem, CancellationToken ct = default);
        Task Delete(string productName, CancellationToken ct = default);
    }
}
