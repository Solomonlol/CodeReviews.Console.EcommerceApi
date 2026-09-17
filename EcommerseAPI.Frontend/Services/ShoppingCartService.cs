using EcommerseAPI.Frontend.Entities;
using EcommerseAPI.Frontend.Entities.Dto;
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
        public Task AddToCart(ProductDto dto, int quantity = 1, CancellationToken ct = default)
        {
            var cartItem = new CartItem();
            var check = _cartList.FirstOrDefault(c => c.Product == dto);
            if (check == null)
            {
                cartItem.Quantity = quantity;
                cartItem.Product = dto;
                _cartList.Add(cartItem);
                return Task.CompletedTask;
            }
            else
            {
                var index = _cartList.IndexOf(check);
                _cartList[index].Quantity += quantity;
                return Task.CompletedTask;
            }
        }

        public List<CartItem> GetList()
        {
            return _cartList;
        }

        public async Task RemoveFromCart(CancellationToken ct = default)
        {
            if (_cartList.Count > 0)
            {
                var list = _cartList.Select(c => c.Product).Cast<ProductDto>().ToList();
                var choises = await AnsiConsole.PromptAsync(new MultiSelectionPrompt<ProductDto>()
                .Title("[yellow]Choose product to remove:[/]")
                .UseConverter(p => $"{p.Id} | {p.Name} | {p.Price} | {p.CategoryName}")
                .AddChoices(list));


                foreach (var choise in choises)
                {
                    var item = _cartList.FirstOrDefault(c => c.Product == choise);
                    if (item != null)
                        _cartList.Remove(item);
                }
            }
            else AnsiConsole.MarkupLine("[red]No product available in cart[/]");
        }

        public Task Clear(CancellationToken ct=default)
        {
            _cartList.Clear();
            return Task.CompletedTask;
        }
    }
}
