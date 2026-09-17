using EcommerseAPI.Frontend.Entities.Dto.Products;
using EcommerseAPI.Frontend.Entities.Dto.Users;
using EcommerseAPI.Frontend.Entities.Sort;
using EcommerseAPI.Frontend.Interfaces;
using Spectre.Console;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using static EcommerseAPI.Frontend.Entities.EnumHelper;

namespace EcommerseAPI.Frontend.Services
{
    internal class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly ITableDrawingService _drawingService;
        private readonly IUrlService _urlService;
        public AccountService(IHttpClientFactory httpClient, ITableDrawingService drawingService, IUrlServiceFactory urlService)
        {
            _urlService = urlService.Create("api/v1/users/");
            _httpClient = httpClient.CreateClient("ApiClient");
            _drawingService = drawingService;
        }
        public async Task Create(UserDtoCreation user, CancellationToken ct = default)
        {
            try
            {
                var Url = await _urlService.GetUrl(ct: ct);
                var dto = JsonSerializer.Serialize(user);
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

        public async Task Delete(string login, CancellationToken ct = default)
        {
            try
            {
                var Url = await _urlService.GetUrl(ct: ct);
                var response = await _httpClient.DeleteAsync(Url + login.Trim(), ct);
                if (response.IsSuccessStatusCode)
                    AnsiConsole.MarkupLine($"[green]Account with login name {login} was deleted.[/]");
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
                var Url = await _urlService.GetUrl(ct: ct, pageSize: pageSize, page: page, sort:sort, filter:filter);
                var response = await _httpClient.GetAsync(Url, ct);
                return response;
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
                return null;
            }
        }

        public async Task GetMe(CancellationToken ct = default)
        {
            try
            {
                var url = await _urlService.GetUrl(ct: ct) + "me";
                var response = await _httpClient.GetAsync(url, ct);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadFromJsonAsync<UserDtoResponse>(ct);
                    if (content != null)
                    {
                        var list = new List<UserDtoResponse>();
                        list.Add(content);
                        await _drawingService.DrowSimpleTable(title: "My account", ct: ct, enumerableValues: list, isNestedDrawing: true);
                    }
                }
                else AnsiConsole.MarkupLine($"[red]Error: {response.StatusCode}[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
            }
        }

        public async Task GetOne(string login, CancellationToken ct = default)
        {
            try
            {
                var url = await _urlService.GetUrl(ct: ct) + $"{login}";
                var response = await _httpClient.GetAsync(url, ct);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadFromJsonAsync<UserDtoResponse>(ct);
                    if (content != null)
                    {
                        var list = new List<UserDtoResponse>();
                        list.Add(content);
                        await _drawingService.DrowSimpleTable(enumerableValues: list, title: $"{login} account", ct: ct, isNestedDrawing: true);
                    }
                }
                else AnsiConsole.MarkupLine($"[red]Error: {response.StatusCode}[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
            }
        }

        public async Task Update(CancellationToken ct = default)
        {
            try
            {
                var login = await AnsiConsole.AskAsync<string>("[yellow]Enter login to update:[/]");
                var url = await _urlService.GetUrl(ct: ct) + $"{login}";
                var response = await _httpClient.GetAsync(url, ct);
                if (response.IsSuccessStatusCode)
                {
                    var updatedUser = await response.Content.ReadFromJsonAsync<UserDtoRequest>(ct);
                    var choises = await AnsiConsole.PromptAsync(new MultiSelectionPrompt<string>()
                        .Title("Choose what to update:")
                        .AddChoices("First Name", "Last Name", "Email", "Phone number", "Role"));
                    foreach (var choice in choises)
                    {
                        switch (choice)
                        {
                            case "First Name":
                                updatedUser?.FirstName = await AnsiConsole.AskAsync<string>($"[yellow]Enter new {choice}:[/]");
                                break;
                            case "Last Name":
                                updatedUser?.LastName = await AnsiConsole.AskAsync<string>($"[yellow]Enter new {choice}:[/]");
                                break;
                            case "Email":
                                updatedUser?.Email = await AnsiConsole.AskAsync<string>($"[yellow]Enter new {choice}:[/]");
                                break;
                            case "Phone number":
                                updatedUser?.PhoneNumber = await AnsiConsole.AskAsync<string>($"[yellow]Enter new {choice}:[/]");
                                break;
                            case "Role":
                                updatedUser?.Role = (await AnsiConsole.PromptAsync(new SelectionPrompt<RoleEnum>()
                                                                                            .Title("Choose category:")
                                                                                            .AddChoices(Enum.GetValues<RoleEnum>()))).ToString();
                                break;
                        }
                    }
                    if (await AnsiConsole.ConfirmAsync("Are you sure?", cancellationToken: ct))
                    {
                        var userDto = JsonSerializer.Serialize(updatedUser);
                        var content = new StringContent(userDto, Encoding.UTF8, "application/json");
                        response = await _httpClient.PutAsync(url, content, ct);
                        if (response.IsSuccessStatusCode)
                            AnsiConsole.MarkupLine($"[green]User with login {login} was successfully updated.[/]");
                        else AnsiConsole.MarkupLine($"{response.StatusCode}");
                    }
                    else AnsiConsole.MarkupLine("[violet]The operation was cancelled.[/]");
                }
            }
            catch(Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
            }
        }
    }
}
