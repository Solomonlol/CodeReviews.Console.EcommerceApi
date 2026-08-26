using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solomonlol.EcommerseApi.Models.Base
{
    public class ProductAttributeValue
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;
        public int ProductAttributeId { get; set; }

        [ForeignKey(nameof(ProductAttributeId))]
        public ProductAttribute ProductAttribute { get; set; } = null!;
        public string Value { get; set; } = null!;
    }
}
