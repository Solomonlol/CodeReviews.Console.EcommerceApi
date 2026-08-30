using Solomonlol.EcommerseApi.Models.Dto;
using Solomonlol.EcommerseApi.MyResults;

namespace Solomonlol.EcommerseApi.Interfaces
{
    public interface IAttributeValueService
    {
        Task<Result> AddAttributeValue(ProductAttributeValueDto item, CancellationToken ct = default);
        Task<Result> DeleteAttributeValue(int productId, int productAttributeId, CancellationToken ct = default);
        Task<Result> UpdateAttributeValue(ProductAttributeValueDto item, CancellationToken ct = default);
    }
}
