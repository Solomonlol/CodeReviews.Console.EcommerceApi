using System.ComponentModel.DataAnnotations;
using Solomonlol.EcommerseApi.Models.Base;
using Solomonlol.EcommerseApi.Models.Dto.Category;

namespace Solomonlol.EcommerseApi.Models.Dto.Product
{
    public class ProductAttributeDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        public string? Unit { get; set; }
    }
}
