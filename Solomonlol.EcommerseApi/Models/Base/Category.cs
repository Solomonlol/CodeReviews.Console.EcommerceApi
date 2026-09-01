using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Solomonlol.EcommerseApi.Models.Base
{
    public class Category
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }= string.Empty;
        public bool IsDeleted { get; set; } = false;
        public ICollection<Product> Products { get; set; } = [];
        public ICollection<ProductAttribute> Attributes { get; set; } = [];
    }
}
