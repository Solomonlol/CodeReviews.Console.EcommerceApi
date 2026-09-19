using System.ComponentModel.DataAnnotations;

namespace EcommerseAPI.Frontend.Entities.Dto.Categories
{
    public class CategoryDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
    }
}
