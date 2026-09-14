using EcommerseAPI.Frontend.Entities.Dto.Categories;
using EcommerseAPI.Frontend.Entities.Dto.Users;
using EcommerseAPI.Frontend.Interfaces;
using EcommerseAPI.Frontend.Services.Factory.Sort;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace EcommerseAPI.Frontend.Services
{
    internal class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ITableDrawingService _drawingService;
        private readonly IUrlService _urlService;
        public CategoryService(IHttpClientFactory httpClient, ITableDrawingService drawingService, IUrlServiceFactory urlService)
        {
            _urlService = urlService.Create("api/v1/categories/");
            _httpClient = httpClient.CreateClient("ApiClient");
            _drawingService = drawingService;
        }
        public async Task Create(CategoryDto category, CancellationToken ct = default)
        {
            var Url = await _urlService.GetUrl(ct: ct);
            var dto = JsonSerializer.Serialize(category);
            var content = new StringContent(dto, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(Url, content, ct);
            if (response.IsSuccessStatusCode)
                AnsiConsole.MarkupLine("[green]Successfully created[/]");
            else AnsiConsole.MarkupLine($"[red]Error: {response.StatusCode}[/]");
        }

        public async Task Delete(string categoryName, CancellationToken ct = default)
        {
            var Url = await _urlService.GetUrl(ct: ct);
            var response = await _httpClient.DeleteAsync(Url + categoryName.Trim(), ct);
            if (response.IsSuccessStatusCode)
                AnsiConsole.MarkupLine($"[green]Category with name '{categoryName}' was deleted.[/]");
            else AnsiConsole.MarkupLine($"[red]Error: {response.StatusCode}[/]");
        }

        public async Task<HttpResponseMessage> GetAll(object? filter = null, SortParams? sort = null, int page = 1, int pageSize = 5, CancellationToken ct = default)
        {
            var Url = await _urlService.GetUrl(ct: ct, pageSize: pageSize, page: page);
            var response = await _httpClient.GetAsync(Url, ct);
            return response;
        }

        public async Task GetOne(string categoryName, CancellationToken ct = default)
        {
            var url = await _urlService.GetUrl(ct: ct) + $"{categoryName}";
            var response = await _httpClient.GetAsync(url, ct);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<CategoryDto>(ct);
                if (content != null)
                {
                    var list = new List<CategoryDto>();
                    list.Add(content);
                    await _drawingService.DrowSimpleTable(enumerableValues: list, title: $"{categoryName}", ct: ct);
                }
            }
            else AnsiConsole.MarkupLine($"[red]Error: {response.StatusCode}[/]");
        }

        public async Task Update(CategoryDto category, CancellationToken ct = default)
        {
            var categoryName = await AnsiConsole.AskAsync<string>("[yellow]Enter category name to update:[/]");
            var url = await _urlService.GetUrl(ct: ct) + $"{categoryName}";
            var userDto = JsonSerializer.Serialize(category);
            var content = new StringContent(userDto, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, content, ct);
            if (response.IsSuccessStatusCode)
                AnsiConsole.MarkupLine($"[green]Category with name '{categoryName}' was successfully updated.[/]");
            else AnsiConsole.MarkupLine($"{response.StatusCode}");
        }
    }
}
