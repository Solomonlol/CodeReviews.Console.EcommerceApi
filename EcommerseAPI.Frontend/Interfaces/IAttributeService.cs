using EcommerseAPI.Frontend.Entities.Dto.Products;

namespace EcommerseAPI.Frontend.Interfaces
{
    internal interface IAttributeService
    {
        Task AddAttribute(IEnumerable<ProductDto> attributes, CancellationToken ct = default);
        Task UpdateAttribute(IEnumerable<ProductDto> products, CancellationToken ct = default);
        Task DeleteAttribute(IEnumerable<ProductDto> attributes, CancellationToken ct = default);


    }
}
