using EcommerseAPI.Frontend.Entities.Dto.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerseAPI.Frontend.Interfaces
{
    internal interface IAttributeService
    {
        Task AddAttribute(IEnumerable<ProductDto> attributes, CancellationToken ct = default);
        Task UpdateAttribute(IEnumerable<ProductDto> products, CancellationToken ct = default);
        Task DeleteAttribute(IEnumerable<ProductDto> attributes, CancellationToken ct = default);

        
    }
}
