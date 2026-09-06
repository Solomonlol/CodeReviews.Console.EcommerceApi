using Solomonlol.EcommerseApi.Models.Dto.Product;

namespace Solomonlol.EcommerseApi.Models.Dto.Sale
{
    public class SaleItemDtoResponse
    {
        public ProductDto Product { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SaleItemPrice { get => UnitPrice * Quantity; }
    }
}
