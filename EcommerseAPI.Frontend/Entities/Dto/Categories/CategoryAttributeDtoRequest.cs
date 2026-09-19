using System.ComponentModel.DataAnnotations;

namespace EcommerseAPI.Frontend.Entities.Dto.Categories
{
    internal class CategoryAttributeDtoRequest
    {
        [Required]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Name required")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Attribute name minimum length = 3, maximum = 20")]
        public string Name { get; set; } = null!;

        [StringLength(20, MinimumLength = 3, ErrorMessage = "Attribute name minimum length = 3, maximum = 20")]
        public string? Unit { get; set; }
    }
}
