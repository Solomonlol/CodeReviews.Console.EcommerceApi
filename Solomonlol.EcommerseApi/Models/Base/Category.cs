using System.ComponentModel.DataAnnotations;

namespace Solomonlol.EcommerseApi.Models.Base
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }= string.Empty;
        public ICollection<Product> Products { get; set; } = [];
        public ICollection<ProductAttribute> Attributes { get; set; } = [];
    }
}
