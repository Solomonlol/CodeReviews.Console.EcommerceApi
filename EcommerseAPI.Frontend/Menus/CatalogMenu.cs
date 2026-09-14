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
                Description = "Test descriprion",
                Price = 10000,
                CategoryId = (int)CategoryEnum.GPU
            };
            await _productService.Create(product, ct);
        }
        public async Task Update(CancellationToken ct=default)
        {
            var product = new ProductDto
            {
                Name = "New Super-Duper Product 2",
                Description = "Test descriprion 2",
                Price = 100000,
                CategoryId = (int)CategoryEnum.CPU
            };
            await _productService.Update(product, ct);
        }

        public async Task Delete(CancellationToken ct=default)
        {
            var productName = AnsiConsole.Ask<string>("Enter product name to delete:");
            await _productService.Delete(productName, ct);
        }

        public async Task ChooseCategory(CancellationToken ct = default)
        {
            var url = "api/v1/categories";
            var response = await _httpClient.GetAsync(url, ct);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<PagedResult<CategoryDto>>(ct);
                if (content.Items.Any())
                {

                    await _drowingService.DrowSimpleTable(content, "Products", ct);
                }
            }
            else AnsiConsole.MarkupLine($"{response.StatusCode}");
        }
    }
}
