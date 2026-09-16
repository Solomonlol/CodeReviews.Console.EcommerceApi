using EcommerseAPI.Frontend.Entities;
using EcommerseAPI.Frontend.Entities.Dto;
using EcommerseAPI.Frontend.Entities.Dto.Categories;
using EcommerseAPI.Frontend.Entities.Dto.Products;
using EcommerseAPI.Frontend.Interfaces;
using EcommerseAPI.Frontend.Services.Factory.Filters;
using Spectre.Console;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using static EcommerseAPI.Frontend.Entities.EnumHelper;

namespace EcommerseAPI.Frontend.Menus
{
    internal class CatalogMenu : UserInterface
    {
        private readonly HttpClient _httpClient;
        private readonly ITableDrawingService _drowingService;
        private readonly IUrlService _urlService;
        private readonly IProductService _productService;
        public CatalogMenu(IProductService productService, IHttpClientFactory clientFactory, ITableDrawingService drowingService, IUrlServiceFactory serviceFactory, IServiceProvider sp, ITableDrawingService drawingService) : base("Catalog")
        {
            _urlService = serviceFactory.Create("api/v1/products/");
            _drowingService = drowingService;
            _httpClient = clientFactory.CreateClient("ApiClient");
            _productService = productService;
            AddSubMenu("All products", new PagedMenu<IProductService, ProductDto, ProductFilter>(sp, drawingService, "Products"));
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
            await _productService.Create(product, ct);
        }
        public async Task Update(CancellationToken ct=default)
        {
            await _productService.Update(ct);
        }

        public async Task Delete(CancellationToken ct=default)
        {
            var productName = AnsiConsole.Ask<string>("Enter product name to delete:");
            await _productService.Delete(productName, ct);
        }
    }
}
