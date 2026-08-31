using Solomonlol.EcommerseApi.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Solomonlol.EcommerseApi.Models.Dto
{
    public class ProductDto
    {
        int Id { get; set; }
        public string Name { get; set; } = null!;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<ProductAttributeDisplayDto> Attributes { get; set; } = [];
        [Required]
        public int CategoryId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

    }
}
