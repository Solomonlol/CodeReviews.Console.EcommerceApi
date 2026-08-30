using Microsoft.AspNetCore.Mvc.ModelBinding;
using Solomonlol.EcommerseApi.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Solomonlol.EcommerseApi.Models.Dto
{
    public class ProductAttributeDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string? Unit { get; set; }
    }
}
