using EcommerseAPI.Frontend.Entities.Dto.Products;
using EcommerseAPI.Frontend.Entities.Dto.Sales;
using EcommerseAPI.Frontend.Interfaces;
using EcommerseAPI.Frontend.Services.Factory.Filters;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;
using static EcommerseAPI.Frontend.Entities.EnumHelper;

namespace EcommerseAPI.Frontend.Menus
{
    internal class SaleMenu : UserInterface
    {
        private readonly HttpClient _httpClient;
        private readonly ITableDrawingService _drowingService;
        private readonly IUrlService _urlService;
        private readonly IProductService _productService;
        public SaleMenu(IProductService productService, IHttpClientFactory clientFactory, ITableDrawingService drowingService, IUrlServiceFactory serviceFactory, IServiceProvider sp, ITableDrawingService drawingService) : base("Catalog")
        {
            _urlService = serviceFactory.Create("api/v1/products/");
            _drowingService = drowingService;
            _httpClient = clientFactory.CreateClient("ApiClient");
            _productService = productService;
            AddSubMenu("All sales", new PagedMenu<ISaleService, SaleDtoResponse, SaleFilter>(sp, drawingService, "Sales"));
            AddItem("Find one", () => Get());
            AddItem("Create new sale", () => Create());
            AddItem("Close sale", () => Close());
            AddExitOption("Back");
        }

        public async Task Get(CancellationToken ct = default)
        {
            var productName = await AnsiConsole.AskAsync<string>("Enter product name to find:");
            await _productService.GetOne(productName, ct);
        }

        public async Task Create(CancellationToken ct = default)
        {
            //var product = new SaleDtoRequest
            //{
            //    //Name = AnsiConsole.Ask<string>("Enter name:"),
            //    //Description = AnsiConsole.Ask<string>("Enter description:"),
            //    //Price = AnsiConsole.Ask<int>("Enter price:"),
            //    //CategoryId = (int)AnsiConsole.Prompt(new SelectionPrompt<CategoryEnum>()
            //    //.Title("Choose category:")
            //    //.AddChoices(Enum.GetValues<CategoryEnum>()))
            //    UserId = AnsiConsole.Ask<string>("Enter name:")
            //};

        }

        public async Task Close(CancellationToken ct = default)
        {
            var productName = AnsiConsole.Ask<string>("Enter product name to delete:");
            if (await AnsiConsole.ConfirmAsync("Are you sure?", cancellationToken: ct))
                await _productService.Delete(productName, ct);
            else AnsiConsole.MarkupLine("[violet]The operation was cancelled.[/]");
        }
    }
}
