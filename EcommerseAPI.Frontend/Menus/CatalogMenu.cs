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
        public CatalogMenu(IHttpClientFactory clientFactory, ITableDrawingService drowingService) : base("Catalog")
        {
            _drowingService = drowingService;
            _httpClient = clientFactory.CreateClient("ApiClient");
            AddExitOption("Back");
            AddItem("All products list", () => ShowAllProducts());
            AddItem("Choose category", () => ChooseCategory());
        }


        public async Task ShowAllProducts(CancellationToken ct = default)
        {
            var url = "api/v1/products";
            var response = await _httpClient.GetAsync(url, ct);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<PagedResult<ProductDto>>(ct);
                if (content.Items.Any())
                {
                    var list = content.Items.ToList();
                    await _drowingService.DrowSimpleTable(list, "Products", ct);
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
                    var list = content.Items.ToList();
                    await _drowingService.DrowSimpleTable(list, "Products", ct);
                }
            }
            else AnsiConsole.MarkupLine($"{response.StatusCode}");
        }
    }
}
