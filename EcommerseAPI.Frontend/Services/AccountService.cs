using EcommerseAPI.Frontend.Entities.Dto.Users;
using EcommerseAPI.Frontend.Interfaces;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace EcommerseAPI.Frontend.Services
{
    internal class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        public AccountService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("ApiClient");
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
                AnsiConsole.MarkupLine($"{response.Content}");
            else AnsiConsole.MarkupLine($"[red]Error: {response.StatusCode}[/]");
        }

        public async Task GetMe(CancellationToken ct = default)
        {
            var url = "api/v1/users/me";
            var response = await _httpClient.GetAsync(url, ct);
            if (response.IsSuccessStatusCode)
                AnsiConsole.MarkupLine($"{response.Content}");
            else AnsiConsole.MarkupLine($"[red]Error: {response.StatusCode}[/]");
        }

        public Task GetOne(string login, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task Update(UserDtoRequest user, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
