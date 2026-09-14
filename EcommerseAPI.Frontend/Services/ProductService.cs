using EcommerseAPI.Frontend.Entities.Dto.Products;
using EcommerseAPI.Frontend.Entities.Dto.Users;
using EcommerseAPI.Frontend.Interfaces;
using EcommerseAPI.Frontend.Services.Factory.Sort;
using Spectre.Console;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace EcommerseAPI.Frontend.Services
{
    internal class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly IUrlService _urlService;
        private readonly ITableDrawingService _drawingService;

        public ProductService(IUrlServiceFactory urlServiceFactory, IHttpClientFactory clientFactory, ITableDrawingService drawingService)
        {
            _urlService = urlServiceFactory.Create("api/v1/products/");
            _httpClient = clientFactory.CreateClient("ApiClient");
            _drawingService = drawingService;
        }
        public async Task Create(ProductDto productItem, CancellationToken ct = default)
        {
            var Url = await _urlService.GetUrl(ct: ct);
            var dto = JsonSerializer.Serialize(productItem);
            var content = new StringContent(dto, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(Url, content, ct);
            if (response.IsSuccessStatusCode)
                AnsiConsole.MarkupLine("[green]Successfully created[/]");
            else AnsiConsole.MarkupLine($"[red]Error: {response.StatusCode}[/]");
        }

        public async Task Delete(string productName, CancellationToken ct = default)
        {
            var Url = await _urlService.GetUrl(ct: ct);
            var response = await _httpClient.DeleteAsync(Url + productName.Trim(), ct);
            if (response.IsSuccessStatusCode)
                AnsiConsole.MarkupLine($"[green]Product with name '{productName}' was deleted.[/]");
            else AnsiConsole.MarkupLine($"[red]Error: {response.StatusCode}[/]");
        }

        public async Task<HttpResponseMessage> GetAll(object? filter = null, SortParams? sort = null, int page = 1, int pageSize = 5, CancellationToken ct = default)
        {
            var Url = await _urlService.GetUrl(ct: ct, pageSize: pageSize, page: page);
            var response = await _httpClient.GetAsync(Url, ct);
            return response;
        }

        public async Task GetOne(string productName, CancellationToken ct = default)
        {
            var url = await _urlService.GetUrl(ct: ct) + $"{productName}";
            var response = await _httpClient.GetAsync(url, ct);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<ProductDto>(ct);
                if (content != null)
                {
                    var list = new List<ProductDto>();
                    list.Add(content);
                    await _drawingService.DrowSimpleTable(enumerableValues: list, title: $"{productName}", ct: ct);
                }
            }
            else AnsiConsole.MarkupLine($"[red]Error: {response.StatusCode}[/]");
        }

        public async Task Update(ProductDto productItem, CancellationToken ct = default)
        {
            var productName = await AnsiConsole.AskAsync<string>("[yellow]Enter product name to update:[/]");
            var url = await _urlService.GetUrl(ct: ct) + $"{productName}";
            var productDtoSerialized = JsonSerializer.Serialize(productItem);
            var content = new StringContent(productDtoSerialized, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, content, ct);
            if (response.IsSuccessStatusCode)
                AnsiConsole.MarkupLine($"[green]User with login {productName} was successfully updated.[/]");
            else AnsiConsole.MarkupLine($"{response.StatusCode}");
        }
    }
}
