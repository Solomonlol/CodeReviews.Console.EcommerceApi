using EcommerseAPI.Frontend.Entities.Dto;
using EcommerseAPI.Frontend.Entities.Dto.Products;
using EcommerseAPI.Frontend.Interfaces;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace EcommerseAPI.Frontend.Menus
{
    internal class CatalogMenu : UserInterface
    {
        private readonly HttpClient _httpClient;
        private readonly ITableDrawingService _drowingService;
        private readonly IUrlService _urlService;
        public CatalogMenu(IHttpClientFactory clientFactory, ITableDrawingService drowingService, IUrlServiceFactory serviceFactory) : base("Catalog")
        {
            _urlService = serviceFactory.Create("api/v1/products/");
            _drowingService = drowingService;
            _httpClient = clientFactory.CreateClient("ApiClient");
            AddExitOption("Back");
            AddItem("All products list", () => ShowAllProducts());
            AddItem("Choose category", () => ChooseCategory());
        }


        public async Task ShowAllProducts(CancellationToken ct = default)
        {
            var url = await _urlService.GetUrl(ct: ct);
            var response = await _httpClient.GetAsync(url, ct);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<PagedResult<ProductDto>>(ct);
                if (content.Items.Any())
                {
                    await _drowingService.DrowSimpleTable(content, "Products", ct);
                }
            }
            else AnsiConsole.MarkupLine($"{response.StatusCode}");
        }

        public async Task ChooseCategory(CancellationToken ct = default)
        {
            var url = "api/v1/categories";
            var response = await _httpClient.GetAsync(url, ct);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<PagedResult<ProductDto>>(ct);
                if (content.Items.Any())
                {
                    
                    await _drowingService.DrowSimpleTable(content, "Products", ct);
                }
            }
            else AnsiConsole.MarkupLine($"{response.StatusCode}");
        }
    }
}
