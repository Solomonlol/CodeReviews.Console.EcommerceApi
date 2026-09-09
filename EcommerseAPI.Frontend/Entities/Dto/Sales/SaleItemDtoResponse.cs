using EcommerseAPI.Frontend.Entities.Dto.Products;

namespace EcommerseAPI.Frontend.Entities.Dto.Sales
{
    public class SaleItemDtoResponse
    {
        public ProductDto Product { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SaleItemPrice { get => UnitPrice * Quantity; }
    }
}
