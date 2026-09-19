using System.ComponentModel.DataAnnotations.Schema;

namespace Solomonlol.EcommerseApi.Models.Base
{
    public class ProductAttributeValue
    {
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;
        public int CategoryId { get; set; }
        public string ProductAttributeName { get; set; } = string.Empty;

        [ForeignKey(nameof(ProductAttributeName))]
        public ProductAttribute ProductAttribute { get; set; } = null!;
        public string Value { get; set; } = null!;
    }
}
