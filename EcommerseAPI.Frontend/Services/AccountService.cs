using EcommerseAPI.Frontend.Entities.Dto;
using EcommerseAPI.Frontend.Entities.Dto.Users;
using EcommerseAPI.Frontend.Interfaces;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace EcommerseAPI.Frontend.Services
{
    internal class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly ITableDrowingService _drowingService;
        public AccountService(IHttpClientFactory httpClient, ITableDrowingService drowingService)
        {
            _httpClient = httpClient.CreateClient("ApiClient");
            _drowingService = drowingService;
        }
        public async Task Create(UserDtoCreation user, CancellationToken ct = default)
        {
            var Url = "api/v1/users";
            var dto = JsonSerializer.Serialize(user);
            var content = new StringContent(dto, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(Url, content, ct);
            if (response.IsSuccessStatusCode)
                AnsiConsole.MarkupLine("[green]Successfully created[/]");
            else AnsiConsole.MarkupLine($"[red]Error: {response.StatusCode}[/]");
        }

        public async Task Delete(string login, CancellationToken ct = default)
        {
            
            var Url = "api/v1/users/";
            var response = await _httpClient.DeleteAsync(Url+login, ct);
            if (response.IsSuccessStatusCode)
                AnsiConsole.MarkupLine($"[green]Account with login name {login} was deleted.[/]");
            else AnsiConsole.MarkupLine($"[red]Error: {response.StatusCode}[/]");
        }

        public async Task GetAll(CancellationToken ct = default)
        {
            var Url = "api/v1/users";
            var response = await _httpClient.GetAsync(Url, ct);
            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<PagedResult<UserDtoResponse>>(ct);
                if (content.Items.Any())
                {
                    var list = content.Items.ToList();
                    await _drowingService.DrowSimpleTable(list, "Users", ct);
                }
            }
            else AnsiConsole.MarkupLine($"[red]Error: {response.StatusCode}[/]");
        }

        public async Task GetMe(CancellationToken ct = default)
        {
            var url = "api/v1/users/me";
            var response = await _httpClient.GetAsync(url, ct);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<UserDtoResponse>(ct);
                if (content!=null)
                {
                    var list = new List<UserDtoResponse>();
                    list.Add(content);
                    await _drowingService.DrowSimpleTable(list, "My account", ct);
                }
            }
            else AnsiConsole.MarkupLine($"[red]Error: {response.StatusCode}[/]");
        }

        public async Task GetOne(string login, CancellationToken ct = default)
        {
            var url = $"api/v1/users/{login}";
            var response = await _httpClient.GetAsync(url, ct);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<UserDtoResponse>(ct);
                if (content != null)
                {
                    var list = new List<UserDtoResponse>();
                    list.Add(content);
                    await _drowingService.DrowSimpleTable(list, $"{login} account", ct);
                }
            }
            else AnsiConsole.MarkupLine($"[red]Error: {response.StatusCode}[/]");
        }

        public async Task Update(UserDtoRequest user, CancellationToken ct = default)
        {
            var login = await AnsiConsole.AskAsync<string>("[yellow]Enter login to update:[/]");
            var url = $"api/v1/users/{login}";
            var userDto = JsonSerializer.Serialize(user);
            var content = new StringContent(userDto, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, content, ct);
            if (response.IsSuccessStatusCode)
                AnsiConsole.MarkupLine($"[green]User with login {login} was successfully updated.[/]");
            else AnsiConsole.MarkupLine($"{response.StatusCode}");
        }
    }
}
