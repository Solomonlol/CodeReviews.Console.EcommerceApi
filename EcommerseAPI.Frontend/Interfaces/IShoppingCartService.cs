using EcommerseAPI.Frontend.Entities;
using EcommerseAPI.Frontend.Entities.Dto.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerseAPI.Frontend.Interfaces
{
    internal interface IShoppingCartService
    {
        Task AddToCart(ProductDto dto, int quantity = 1, CancellationToken ct = default);
        Task RemoveFromCart(CancellationToken ct = default);
        List<CartItem> GetList();
        Task Clear(CancellationToken ct = default);
    }
}
