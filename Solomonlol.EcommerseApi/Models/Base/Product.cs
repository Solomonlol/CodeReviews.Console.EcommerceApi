using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solomonlol.EcommerseApi.Models.Base
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; } = false;

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;
    }
}
