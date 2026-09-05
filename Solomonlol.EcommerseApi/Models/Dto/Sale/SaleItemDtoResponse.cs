using Solomonlol.EcommerseApi.Models.Dto.Product;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solomonlol.EcommerseApi.Models.Dto.Sale
{
    public class SaleItemDtoResponse
    {
        //public int ProductId { get; set; }
        //[ForeignKey(nameof(ProductId))]
        public ProductDto Product { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SaleItemPrice { get => UnitPrice * Quantity; }
    }
}
