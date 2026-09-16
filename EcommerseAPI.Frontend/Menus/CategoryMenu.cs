using EcommerseAPI.Frontend.Entities.Dto.Categories;
using EcommerseAPI.Frontend.Entities.Dto.Users;
using EcommerseAPI.Frontend.Interfaces;
using EcommerseAPI.Frontend.Services.Factory.Filters;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerseAPI.Frontend.Menus
{
    internal class CategoryMenu : UserInterface
    {
        private readonly ICategoryService _categoryService;
        private readonly ILoginService _loginService;

        public CategoryMenu(ICategoryService categoryService, ILoginService loginService, IServiceProvider sp, ITableDrawingService drawingService) : base("Category menu")
        {
            _categoryService = categoryService;
            _loginService = loginService;
            AddExitOption("Back");
            AddSubMenu("Show all categories", new PagedMenu<ICategoryService, CategoryDto, CategoryFilter>(sp, drawingService, "Categories"));
            AddItem("Find one", () => Get());
            AddItem("Create new", () => Create());
            AddItem("Update", () => Update());
            AddItem("Delete", () => Delete());
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
            await _categoryService.GetOne(categoryName, ct);
        }
    }
}
