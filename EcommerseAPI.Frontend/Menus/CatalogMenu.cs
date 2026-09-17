using EcommerseAPI.Frontend.Entities;
using EcommerseAPI.Frontend.Entities.Dto;
using EcommerseAPI.Frontend.Entities.Dto.Categories;
using EcommerseAPI.Frontend.Entities.Dto.Products;
using EcommerseAPI.Frontend.Entities.Filters;
using EcommerseAPI.Frontend.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using static EcommerseAPI.Frontend.Entities.EnumHelper;

namespace EcommerseAPI.Frontend.Menus
{
    internal class CatalogMenu : UserInterface
    {
        private readonly HttpClient _httpClient;
        private readonly ITableDrawingService _drawingService;
        private readonly IUrlService _urlService;
        private readonly IProductService _productService;
        public CatalogMenu(IProductService productService, 
            IHttpClientFactory clientFactory, 
            IUrlServiceFactory serviceFactory, 
            IServiceProvider sp,
            IShoppingCartService cartService, 
            ITableDrawingService drawingService,
            SaleMenu saleMenu) : base("Catalog")
        {
            _urlService = serviceFactory.Create("api/v1/products/");
            _drawingService = drawingService;
            _httpClient = clientFactory.CreateClient("ApiClient");
            _productService = productService;
            AddSubMenu("All products", new PagedMenu<IProductService, ProductDto, ProductFilter>(sp, drawingService, cartService, saleMenu, "Products"));
            AddItem("Find one", () => Get());
            AddItem("Create new product", () => Create());
            AddItem("Update product", () => Update());
            AddItem("Delete product", () => Delete());
            AddExitOption("Back");
        }

        public async Task Get(CancellationToken ct=default)
        {
            var productName = await AnsiConsole.AskAsync<string>("Enter product name to find:");
            await _productService.GetOne(productName, ct);
        }

        public async Task Create(CancellationToken ct=default)
        {
            var product = new ProductDto
            {
                Name = AnsiConsole.Ask<string>("Enter name:"),
                Description = AnsiConsole.Ask<string>("Enter description:"),
                Price = AnsiConsole.Ask<int>("Enter price:"),
                CategoryId = (int)AnsiConsole.Prompt(new SelectionPrompt<CategoryEnum>()
                .Title("Choose category:")
                .AddChoices(Enum.GetValues<CategoryEnum>()))
            };

            if (await AnsiConsole.ConfirmAsync("Are you sure?", cancellationToken: ct))
                await _productService.Create(product, ct);
            else AnsiConsole.MarkupLine("[violet]The operation was cancelled.[/]");
        }
        public async Task Update(CancellationToken ct = default)
        {
            await _productService.Update(ct);
        }

        public async Task Delete(CancellationToken ct=default)
        {
            var productName = AnsiConsole.Ask<string>("Enter product name to delete:");
            if (await AnsiConsole.ConfirmAsync("Are you sure?", cancellationToken: ct))
                await _productService.Delete(productName, ct);
            else AnsiConsole.MarkupLine("[violet]The operation was cancelled.[/]");
        }
    }
}
