using EcommerseAPI.Frontend.Entities.Dto.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerseAPI.Frontend.Entities
{
    internal class CartItem
    {
        public int? Quantity { get; set; } = null!;
        public ProductDto? Product { get; set; } = null!;
    }
}
