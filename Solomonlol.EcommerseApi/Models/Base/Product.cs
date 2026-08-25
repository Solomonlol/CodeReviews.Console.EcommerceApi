using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solomonlol.EcommerseApi.Models.Base
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Undefined";
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }

        [Required]
        public int CategoryId { get; set; }
        
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
    }
}
