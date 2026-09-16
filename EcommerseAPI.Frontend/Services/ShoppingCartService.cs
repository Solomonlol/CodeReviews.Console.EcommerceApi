using EcommerseAPI.Frontend.Entities;
using EcommerseAPI.Frontend.Entities.Dto.Products;
using EcommerseAPI.Frontend.Interfaces;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerseAPI.Frontend.Services
{
    internal class ShoppingCartService : IShoppingCartService
    {
        private List<CartItem> _cartList = new();
        public void AddToCart(ProductDto dto, int quantity = 1, CancellationToken ct = default)
        {
            var cartItem = new CartItem();
            var check = _cartList.FirstOrDefault(c => c.Product == dto);
            if (check == null)
            {
                cartItem.Quantity = quantity;
                cartItem.Product = dto;
                _cartList.Add(cartItem);
            }
            else
            {
                var index = _cartList.IndexOf(check);
                _cartList[index].Quantity += quantity;
            }
        }

        public async Task RemoveFromCart(CancellationToken ct = default)
        {
            var choises = await AnsiConsole.PromptAsync(new MultiSelectionPrompt<ProductDto>()
                .Title("[yellow]Choose product to remove:[/]")
                .UseConverter(p =>$"{p.Id} | {p.Name} | {p.Price} | {p.CategoryName}")
                .AddChoices(_cartList.Select(c => c.Product)));

            foreach (var choise in choises)
            {
                var item = _cartList.FirstOrDefault(c => c.Product == choise);
                if(item!=null)
                    _cartList.Remove(item);
            }
        }

        public void Clear()
        {
            _cartList.Clear();
        }
    }
}
