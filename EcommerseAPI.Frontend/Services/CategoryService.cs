using EcommerseAPI.Frontend.Entities.Dto.Categories;
using EcommerseAPI.Frontend.Entities.Sort;
using EcommerseAPI.Frontend.Interfaces;
using EcommerseAPI.Frontend.MyValidation;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using System.Text;
using System.Text.Json;

namespace EcommerseAPI.Frontend.Services
{
    internal class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ITableDrawingService _drawingService;
        private readonly IUrlService _urlService;
        public CategoryService(IServiceProvider sp)
        {
            _urlService = sp.GetRequiredService<IUrlServiceFactory>().Create("api/v1/categories/");
            _httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiClient");
            _drawingService = sp.GetRequiredService<ITableDrawingService>();
        }
        public async Task Create(CategoryDto category, CancellationToken ct = default)
        {
            try
            {
                var Url = await _urlService.GetUrl(ct: ct);
                var dto = JsonSerializer.Serialize(category);
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

        public async Task Delete(string categoryName, CancellationToken ct = default)
        {
            try
            {
                var Url = await _urlService.GetUrl(ct: ct);
                var response = await _httpClient.DeleteAsync(Url + categoryName.Trim(), ct);
                if (response.IsSuccessStatusCode)
                    AnsiConsole.MarkupLine($"[green]Category with name '{categoryName}' was deleted.[/]");
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
                var Url = await _urlService.GetUrl(ct: ct, pageSize: pageSize, page: page, filter: filter, sort: sort);
                var response = await _httpClient.GetAsync(Url, ct);
                return response;
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
                return null;
            }
        }

        public async Task<HttpResponseMessage?> GetOne(string categoryName, CancellationToken ct = default)
        {
            try
            {
                var url = await _urlService.GetUrl(ct: ct) + $"{categoryName}";
                var response = await _httpClient.GetAsync(url, ct);
                return response;
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
                var categoryName = await AnsiConsole.AskAsync<string>("[yellow]Enter category name to update:[/]");
                var url = await _urlService.GetUrl(ct: ct) + $"{categoryName}";
                var response = await _httpClient.GetAsync(url, ct);
                if (response.IsSuccessStatusCode)
                {
                    var updatedCategory = new CategoryDto();
                    var choises = await AnsiConsole.PromptAsync(new MultiSelectionPrompt<string>()
                        .Title("Choose what to update:")
                        .AddChoices("Name", "Description"));
                    do
                    {
                        foreach (var choice in choises)
                        {
                            switch (choice)
                            {
                                case "Name":
                                    updatedCategory.Name = await AnsiConsole.AskAsync<string>($"[yellow]Enter new product {choice}:[/]");
                                    break;
                                case "Description":
                                    updatedCategory.Description = await AnsiConsole.AskAsync<string>($"[yellow]Enter new product {choice}:[/]");
                                    break;
                            }
                        }
                    }
                    while (!await MyValidations.Validate(updatedCategory));

                    if (await AnsiConsole.ConfirmAsync("Are you sure?", cancellationToken: ct))
                    {
                        var categoryDto = JsonSerializer.Serialize(updatedCategory);
                        var content = new StringContent(categoryDto, Encoding.UTF8, "application/json");
                        response = await _httpClient.PutAsync(url, content, ct);
                        if (response.IsSuccessStatusCode)
                            AnsiConsole.MarkupLine($"[green]Category with name '{categoryName}' was successfully updated.[/]");
                        else AnsiConsole.MarkupLine($"{response.StatusCode}");
                    }
                    else AnsiConsole.MarkupLine("[violet]The operation was cancelled.[/]");
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
            }
        }


    }
}
