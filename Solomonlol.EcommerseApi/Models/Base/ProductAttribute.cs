using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Solomonlol.EcommerseApi.Models.Base
{
    public class ProductAttribute
    {
        [Key]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        [JsonIgnore]
        public Category Category { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Unit { get; set; }
        public ICollection<ProductAttributeValue> Values { get; set; } = [];
    }
}
