using EcommerseAPI.Frontend.Entities.Dto.Products;
using EcommerseAPI.Frontend.Entities.Dto.Sales;
using EcommerseAPI.Frontend.Entities.Filters;
using EcommerseAPI.Frontend.Interfaces;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly ITableDrawingService _drawingService;
        private readonly IUrlService _urlService;
        private readonly IShoppingCartService _cartService;
        private readonly ISaleService _saleService;
        public SaleMenu(IProductService productService, 
            IHttpClientFactory clientFactory, 
            ITableDrawingService drawingService, 
            IUrlServiceFactory serviceFactory, 
            IServiceProvider sp,
            IShoppingCartService cartService
            ) : base("Catalog")
        {
            _cartService = cartService;
            _urlService = serviceFactory.Create("api/v1/products/");
            _drawingService = drawingService;
            _httpClient = clientFactory.CreateClient("ApiClient");
            _saleService = sp.GetRequiredService<ISaleService>();

            AddSubMenu("All sales", new PagedMenu<ISaleService, SaleDtoResponse, SaleFilter>(sp, "Sales"));
            AddItem("Find one", () => Get());
            AddItem("Create new sale", () => Create());
            AddItem("Close sale", () => Close());
            AddExitOption("Back");
        }

        public async Task Get(CancellationToken ct = default)
        {
            var id = await AnsiConsole.AskAsync<int>("Enter sale id to find:");
            if (await AnsiConsole.ConfirmAsync("Are you sure?", cancellationToken: ct))
                await _saleService.GetOne(id, ct);
            else AnsiConsole.MarkupLine("[violet]The operation was cancelled.[/]");
        }

        public async Task Create(CancellationToken ct = default)
        {
            await _saleService.Create(ct);
        }

        public async Task Close(CancellationToken ct = default)
        {
            var id = AnsiConsole.Ask<int>("Enter sale id to close:");
            if (await AnsiConsole.ConfirmAsync("Are you sure?", cancellationToken: ct))
                await _saleService.Close(id, ct);
            else AnsiConsole.MarkupLine("[violet]The operation was cancelled.[/]");
        }
    }
}
