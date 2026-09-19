using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerseAPI.Frontend.Entities.Dto.Products
{
    public class ProductAttributeValueDto
    {
        public int ProductId { get; set; }

        public string ProductAttributeName { get; set; } = string.Empty;
        public string Value { get; set; } = null!;
    }
}
