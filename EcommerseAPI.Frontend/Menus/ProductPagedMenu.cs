using EcommerseAPI.Frontend.Entities.Dto.Products;
using EcommerseAPI.Frontend.Entities.Filters;
using EcommerseAPI.Frontend.Interfaces;
using EcommerseAPI.Frontend.Services;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerseAPI.Frontend.Menus
{
    internal class ProductPagedMenu : PagedMenu<IProductService, ProductDto, ProductFilter>
    {
        private protected readonly IShoppingCartService _cartService;
        private protected readonly IAttributeService _attributeService;
        public ProductPagedMenu(IMenu subMenu, string subMenuTitle, IServiceProvider sp, string title) : base(sp, title)
        {
            _attributeService = sp.GetRequiredService<IAttributeService>();
            _cartService = sp.GetRequiredService<IShoppingCartService>();
            AddItem("Add to cart", () => AddToCart());
            AddItem("Remove item from cart", () => RemoveFromCart());
            AddItem("Clear cart", () => ClearCart());
            AddItem("Add new attribute", () => AddNewAttribute());
            AddItem("Update attribute", () => UpdateAttribute());
            AddItem("Delete attribute", () => DeleteAttribute());
            AddSubMenu($"{subMenuTitle}", subMenu);
        }

        private async Task DeleteAttribute()
        {
            throw new NotImplementedException();
        }

        private async Task UpdateAttribute()
        {
            throw new NotImplementedException();
        }

        private async Task AddNewAttribute(CancellationToken ct=default)
        {
            await _attributeService.AddAttribute(ct);
        }

        public async Task AddToCart(CancellationToken ct = default)
        {
            var list = _pagedResult?.Items.Cast<ProductDto>().ToList();
            if (list?.Count > 0)
            {
                var choises = await AnsiConsole.PromptAsync(new MultiSelectionPrompt<ProductDto>()
                .Title("[yellow]Choose what products add to cart:[/]")
                .AddChoices(list)
                .UseConverter(p => $"{p.Name} | {p.Price} | {p.CategoryName}"));

                foreach (var item in choises)
                {
                    var quantity = await AnsiConsole.AskAsync<int>($"[yellow]Enter quantity of {item.Name} to add:[/]");
                    await _cartService.AddToCart(item, quantity, ct);
                }
            }
            
        }

        public async Task ClearCart(CancellationToken ct = default)
        {
            await _cartService.Clear(ct);
        }

        public async Task RemoveFromCart(CancellationToken ct = default)
        {
            await _cartService.RemoveFromCart(ct);
        }
    }
}
