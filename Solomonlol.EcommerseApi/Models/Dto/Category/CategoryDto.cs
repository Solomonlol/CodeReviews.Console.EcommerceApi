using System.ComponentModel.DataAnnotations;

namespace Solomonlol.EcommerseApi.Models.Dto.Category
{
    public class CategoryDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
