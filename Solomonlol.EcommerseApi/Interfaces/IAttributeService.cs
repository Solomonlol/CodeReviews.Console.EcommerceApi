using Solomonlol.EcommerseApi.Models.Dto;
using Solomonlol.EcommerseApi.MyResults;

namespace Solomonlol.EcommerseApi.Interfaces
{
    public interface IAttributeService
    {
        Task<Result> AddAttribute(ProductAttributeDto item, CancellationToken ct = default);
        Task<Result> DeleteAttribute(string name, CancellationToken ct = default);
        Task<Result> UpdateAttribute(ProductAttributeDto item, CancellationToken ct = default);
    }
}
