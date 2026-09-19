using EcommerseAPI.Frontend.Entities.Dto.Categories;
using EcommerseAPI.Frontend.Entities.Dto.Users;
using EcommerseAPI.Frontend.Entities.Filters;
using EcommerseAPI.Frontend.Interfaces;
using EcommerseAPI.Frontend.Services;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace EcommerseAPI.Frontend.Menus
{
    internal class CategoryMenu : UserInterface
    {
        private readonly ICategoryService _categoryService;
        private readonly ILoginService _loginService;
        private readonly IAttrributeService _attrributeService;
        private readonly ITableDrawingService _drawingService;

        public CategoryMenu(IServiceProvider sp) : base("Category menu")
        {
            _drawingService = sp.GetRequiredService<ITableDrawingService>();
            _attrributeService = sp.GetRequiredService<IAttrributeService>();
            _categoryService = sp.GetRequiredService<ICategoryService>();
            _loginService = sp.GetRequiredService<ILoginService>();

            AddSubMenu("Show all categories", new PagedMenu<ICategoryService, CategoryDto, CategoryFilter>(sp, "Categories"));
            AddItem("Find one", () => Get());
            AddItem("Add attribute", () => AddAttribute());
            AddItem("Update attribute", () => UpdateAttribute());
            AddItem("Delete attribute", () => DeleteAttribute());
            AddItem("Create new", () => Create());
            AddItem("Update", () => Update());
            AddItem("Delete", () => Delete());
            AddExitOption("Back");
        }

        private async Task UpdateAttribute()
        {
            throw new NotImplementedException();
        }

        private async Task DeleteAttribute()
        {
            throw new NotImplementedException();
        }

        public async Task Create(CancellationToken ct=default)
        {
            var dto = new CategoryDto()
            {
                Name = await AnsiConsole.AskAsync<string>("Enter category name:"),
                Description = await AnsiConsole.AskAsync<string>("Enter descriprion of this category:")
            };
            if (await AnsiConsole.ConfirmAsync("Are you sure?", cancellationToken: ct))
                await _categoryService.Create(dto, ct);
            else AnsiConsole.MarkupLine("[violet]The operation was cancelled.[/]");
        }

        public async Task Update(CancellationToken ct=default)
        {
             await _categoryService.Update(ct);
        }

        public async Task Delete(CancellationToken ct=default)
        {
            var categoryName = await AnsiConsole.AskAsync<string>("Enter category name to delete:");
            if(await AnsiConsole.ConfirmAsync("Are you sure?", cancellationToken: ct))
                await _categoryService.Delete(categoryName, ct);
            else AnsiConsole.MarkupLine("[violet]The operation was cancelled.[/]");
        }

        public async Task Get(CancellationToken ct=default)
        {
            var categoryName = await AnsiConsole.AskAsync<string>("Enter category name to find:");
            var response = await _categoryService.GetOne(categoryName, ct);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<CategoryDto>(ct);
                if (content != null)
                {
                    var list = new List<CategoryDto>();
                    list.Add(content);
                    await _drawingService.DrowSimpleTable(enumerableValues: list, title: $"{categoryName}", ct: ct);
                }
            }
            else AnsiConsole.MarkupLine($"[red]Error: {response.StatusCode}[/]");
        }

        public async Task AddAttribute(CancellationToken ct=default)
        {
            await _attrributeService.AddAttribute(ct);
        }
        
    }
}
