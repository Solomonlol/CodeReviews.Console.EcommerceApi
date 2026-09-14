using EcommerseAPI.Frontend.Entities;
using EcommerseAPI.Frontend.Interfaces;
using Spectre.Console;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace EcommerseAPI.Frontend.Services
{
    internal class LoginService : ILoginService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;
        public LoginService(IHttpClientFactory clientFactory, ITokenService tokenService)
        {
            _httpClient = clientFactory.CreateClient("ApiClient");
            _tokenService = tokenService;
        }
        public async Task LogIn(LoginRequest request, CancellationToken ct = default)
        {
            var url = "api/v1/login";
            var jsonDto = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonDto, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content, ct);
            if (response.IsSuccessStatusCode)
            {
                AnsiConsole.MarkupLine($"[green]Authorized[/]");
                var contentString = await response.Content.ReadFromJsonAsync<string>(ct);
                if (!string.IsNullOrEmpty(contentString))
                    await _tokenService.SaveToken(contentString, ct);
            }
            else AnsiConsole.MarkupLine($"[red]Error: {response.StatusCode}[/]");
        }

        public async Task LogOut(CancellationToken ct = default)
        {
            await _tokenService.SaveToken("");
            AnsiConsole.MarkupLine("[red]You have logged out.[/]");
        }
    }
}
