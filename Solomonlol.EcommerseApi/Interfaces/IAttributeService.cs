using Solomonlol.EcommerseApi.Models.Dto.Product;
using Solomonlol.EcommerseApi.MyResults;

namespace Solomonlol.EcommerseApi.Interfaces
{
    public interface IAttributeService
    {
        Task<Result> AddAttribute(string categoryName, ProductAttributeDto item, CancellationToken ct = default);
        Task<Result> DeleteAttribute(string categoryName, string attributeName, CancellationToken ct = default);
        Task<Result> UpdateAttribute(string categoryName, string attributeName, ProductAttributeDto item, CancellationToken ct = default);
    }
}
