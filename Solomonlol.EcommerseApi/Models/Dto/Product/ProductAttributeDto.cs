using Solomonlol.EcommerseApi.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Solomonlol.EcommerseApi.Models.Dto.Product
{
    public class ProductAttributeDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        public string? Unit { get; set; }
    }
}
