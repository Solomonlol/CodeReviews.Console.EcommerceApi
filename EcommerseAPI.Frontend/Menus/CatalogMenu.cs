using EcommerseAPI.Frontend.Entities.Dto.Products;
using EcommerseAPI.Frontend.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using static EcommerseAPI.Frontend.Entities.EnumHelper;

namespace EcommerseAPI.Frontend.Menus
{
    internal class CatalogMenu : UserInterface
    {
        private readonly HttpClient _httpClient;
        private readonly ITableDrawingService _drawingService;
        private readonly IUrlService _urlService;
        private readonly IProductService _productService;
        public CatalogMenu(IServiceProvider sp, SaleMenu saleMenu, CategoryMenu categoryMenu) : base("Catalog")
        {
            _urlService = sp.GetRequiredService<IUrlServiceFactory>().Create("api/v1/products/");
            _httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiClient");
            _drawingService = sp.GetRequiredService<ITableDrawingService>();
            _productService = sp.GetRequiredService<IProductService>();

            AddSubMenu("All products", new ProductPagedMenu(saleMenu, "Sale menu", sp, "Products"));
            AddSubMenu("Category management", categoryMenu);
            AddItem("Find one", () => Get());
            AddItem("Create new product", () => Create());
            AddItem("Update product", () => Update());
            AddItem("Delete product", () => Delete());
            AddExitOption("Back");
        }

        public async Task Get(CancellationToken ct = default)
        {
            var productName = await AnsiConsole.AskAsync<string>("[yellow]Enter product name to find:[/]");
            await _productService.GetOne(productName, ct);
        }

        public async Task Create(CancellationToken ct = default)
        {
            var product = new ProductDto
            {
                Name = AnsiConsole.Ask<string>("[yellow]Enter name:[/]"),
                Description = AnsiConsole.Ask<string>("[yellow]Enter description:[/]"),
                Price = AnsiConsole.Ask<int>("[yellow]Enter price:[/]"),
                CategoryId = (int)AnsiConsole.Prompt(new SelectionPrompt<CategoryEnum>()
                .Title("[yellow]Choose category:[/]")
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

        public async Task Delete(CancellationToken ct = default)
        {
            var productName = AnsiConsole.Ask<string>("[yellow]Enter product name to delete:[/]");
            if (await AnsiConsole.ConfirmAsync("Are you sure?", cancellationToken: ct))
                await _productService.Delete(productName, ct);
            else AnsiConsole.MarkupLine("[violet]The operation was cancelled.[/]");
        }


    }
}
