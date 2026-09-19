using EcommerseAPI.Frontend.Entities.Dto;
using EcommerseAPI.Frontend.Entities.Dto.Categories;
using EcommerseAPI.Frontend.Entities.Dto.Users;
using EcommerseAPI.Frontend.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace EcommerseAPI.Frontend.Services
{
    internal class AttributeService : IAttributeService
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly ITableDrawingService _drawingService;
        public AttributeService(IServiceProvider sp) 
        {
            _drawingService = sp.GetRequiredService<ITableDrawingService>();
            _categoryService = sp.GetRequiredService<ICategoryService>();
            _productService = sp.GetRequiredService<IProductService>();
        }

        public async Task AddAttribute(CancellationToken ct = default)
        {
            try
            {
                var categoryName = await AnsiConsole.AskAsync<string>("[yellow]Enter category name:[/]");
                var response = await _categoryService.GetOne(categoryName, ct);
                if (response == null)
                {
                    AnsiConsole.MarkupLine($"[red]Not found category with name '{categoryName}'[/]");
                    return;
                }
                if (!response.IsSuccessStatusCode)
                {
                    AnsiConsole.MarkupLine($"[red]Error: {response.StatusCode}[/]");
                    return;
                }

                var content = await response.Content.ReadFromJsonAsync<CategoryDto>(ct);


                content = await response.Content.ReadFromJsonAsync<CategoryDto>(ct);
                if (content != null)
                {
                    var list = new List<CategoryDto>();
                    list.Add(content);
                    await _drawingService.DrowSimpleTable(enumerableValues: list, title: $"{categoryName}", ct: ct);
                }

            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
            }
        }

        public Task AddAttributeValue(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAttribute(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAttribute(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAttributeValue(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
