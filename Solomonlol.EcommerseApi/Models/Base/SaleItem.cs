using System.ComponentModel.DataAnnotations.Schema;

namespace Solomonlol.EcommerseApi.Models.Base
{
    public class SaleItem
    {
        public int SaleId { get; set; }
        [ForeignKey(nameof(SaleId))]
        public Sale Sale { get; set; } = null!;
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
    }
}
