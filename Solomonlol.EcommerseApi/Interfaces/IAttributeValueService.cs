using Solomonlol.EcommerseApi.Models.Dto.Product;
using Solomonlol.EcommerseApi.MyResults;

namespace Solomonlol.EcommerseApi.Interfaces
{
    public interface IAttributeValueService
    {
        Task<Result> AddAttributeValue(string productName, ProductAttributeValueDto item, CancellationToken ct = default);
        Task<Result> DeleteAttributeValue(string productName, string productAttributeName, CancellationToken ct = default);
        Task<Result> UpdateAttributeValue(string productName, string productAttributeName, ProductAttributeValueDto item, CancellationToken ct = default);
    }
}
