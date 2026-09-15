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
        }

        public async Task Create(CancellationToken ct=default)
        {
            var dto = new CategoryDto()
            {
                Name = await AnsiConsole.AskAsync<string>("Enter category name:"),
                Description = await AnsiConsole.AskAsync<string>("Enter descriprion of this category:")
            };
            await _categoryService.Create(dto, ct);
        }

        public async Task Update(CancellationToken ct=default)
        {
            var dto = new CategoryDto()
            {
                Name = await AnsiConsole.AskAsync<string>("Enter new category name:"),
                Description = await AnsiConsole.AskAsync<string>("Enter new descriprion of this category:")
            };
            //_categoryService.Update()
        }
    }
}
