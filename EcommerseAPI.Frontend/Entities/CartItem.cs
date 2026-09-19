using EcommerseAPI.Frontend.Entities.Dto.Products;

namespace EcommerseAPI.Frontend.Entities
{
    internal class CartItem
    {
        public int? Quantity { get; set; } = null!;
        public ProductDto? Product { get; set; } = null!;
    }
}
