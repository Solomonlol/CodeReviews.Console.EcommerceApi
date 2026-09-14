using EcommerseAPI.Frontend.Entities.Dto.Categories;
using EcommerseAPI.Frontend.Entities.Dto.Users;
using EcommerseAPI.Frontend.Interfaces;
using EcommerseAPI.Frontend.Services.Factory.Filters;
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
    }
}
