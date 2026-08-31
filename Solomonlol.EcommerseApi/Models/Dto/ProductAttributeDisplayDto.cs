using Solomonlol.EcommerseApi.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solomonlol.EcommerseApi.Models.Dto
{
    public class ProductAttributeDisplayDto
    {
        public string Name { get; set; } = null!;
        public string? Unit { get; set; }
        public string? Value { get; set; }

    }
}
