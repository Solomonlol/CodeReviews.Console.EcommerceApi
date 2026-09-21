using EcommerseAPI.Frontend.Entities.Dto.Products;
using EcommerseAPI.Frontend.Entities.Sort;
using EcommerseAPI.Frontend.Interfaces;
using EcommerseAPI.Frontend.MyValidation;
using Spectre.Console;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using static EcommerseAPI.Frontend.Entities.EnumHelper;

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
            try
            {
                var Url = await _urlService.GetUrl(ct: ct);
                var dto = JsonSerializer.Serialize(productItem);
                var content = new StringContent(dto, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(Url, content, ct);
                if (response.IsSuccessStatusCode)
                    AnsiConsole.MarkupLine("[green]Successfully created[/]");
                else AnsiConsole.MarkupLine($"[red]Error: {response.StatusCode}[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
            }
        }

        public async Task Delete(string productName, CancellationToken ct = default)
        {
            try
            {
                var Url = await _urlService.GetUrl(ct: ct);
                var response = await _httpClient.DeleteAsync(Url + productName.Trim(), ct);
                if (response.IsSuccessStatusCode)
                    AnsiConsole.MarkupLine($"[green]Product with name '{productName}' was deleted.[/]");
                else AnsiConsole.MarkupLine($"[red]Error: {response.StatusCode}[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
            }
        }

        public async Task<HttpResponseMessage?> GetAll(IFilter? filter = null, SortParams? sort = null, int page = 1, int pageSize = 5, CancellationToken ct = default)
        {
            try
            {
                var Url = await _urlService.GetUrl(ct: ct, pageSize: pageSize, page: page, sort: sort, filter: filter);
                var response = await _httpClient.GetAsync(Url, ct);
                return response;
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
                return null;
            }
        }

        public async Task<ProductDto?> GetOne(string productName, CancellationToken ct = default)
        {
            try
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
                        await _drawingService.DrowSimpleTable(enumerableValues: list, title: $"{productName}", ct: ct, isNestedDrawing: true);

                    }
                    return content;
                }
                AnsiConsole.MarkupLine($"[red]Error: {response.StatusCode}[/]");
                return null;
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
                return null;
            }
        }

        public async Task Update(CancellationToken ct = default)
        {
            try
            {
                var productName = await AnsiConsole.AskAsync<string>("[yellow]Enter product name to update:[/]");
                var url = await _urlService.GetUrl(ct: ct) + $"{productName}";
                var response = await _httpClient.GetAsync(url, ct);
                if (response.IsSuccessStatusCode)
                {
                    var responseDto = await response.Content.ReadFromJsonAsync<ProductDto>();
                    var updatedProduct = responseDto ?? new ProductDto();
                    var choises = await AnsiConsole.PromptAsync(new MultiSelectionPrompt<string>()
                        .Title("Choose what to update:")
                        .AddChoices("Name", "Description", "Price", "Category"));
                    do
                    {
                        foreach (var choice in choises)
                        {
                            switch (choice)
                            {
                                case "Name":
                                    updatedProduct.Name = await AnsiConsole.AskAsync<string>($"[yellow]Enter new product {choice}:[/]");
                                    break;
                                case "Description":
                                    updatedProduct.Description = await AnsiConsole.AskAsync<string>($"[yellow]Enter new product {choice}:[/]");
                                    break;
                                case "Price":
                                    updatedProduct.Price = await AnsiConsole.AskAsync<decimal>($"[yellow]Enter new product {choice}:[/]");
                                    break;
                                case "Category":
                                    updatedProduct.CategoryId = (int)await AnsiConsole.PromptAsync(new SelectionPrompt<CategoryEnum>()
                                                                                                .Title("Choose category:")
                                                                                                .AddChoices(Enum.GetValues<CategoryEnum>()));
                                    break;
                            }
                        }
                    }
                    while (!await MyValidations.Validate(updatedProduct));

                    var productDtoSerialized = JsonSerializer.Serialize(updatedProduct);
                    var content = new StringContent(productDtoSerialized, Encoding.UTF8, "application/json");
                    response = await _httpClient.PutAsync(url, content, ct);
                    if (response.IsSuccessStatusCode)
                        AnsiConsole.MarkupLine($"[green]Product with login {productName} was successfully updated.[/]");
                    else AnsiConsole.MarkupLine($"{response.StatusCode}");
                }
                else AnsiConsole.MarkupLine($"[red]Error: {response.StatusCode}[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
            }
        }
    }
}
