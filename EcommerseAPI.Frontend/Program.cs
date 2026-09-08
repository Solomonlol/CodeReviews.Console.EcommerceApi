using EcommerseAPI.Frontend.Handlers;
using EcommerseAPI.Frontend.Interfaces;
using EcommerseAPI.Frontend.Menus;
using EcommerseAPI.Frontend.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddTransient<TokenHandler>();
        services.AddSingleton<ITokenService, TokenService>();
        services.AddTransient<MainMenu>();
        services.AddTransient<CatalogMenu>();
        services.AddTransient<AccountMenu>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddHttpClient("ApiClient", (sp, client) =>
        {
            var config = sp.GetRequiredService<IConfiguration>();
            var baseUrl = config["ApiSettings:BaseUrl"] ?? "http://localhost:5254/";
            client.BaseAddress = new Uri(baseUrl);
        })
        .AddHttpMessageHandler<TokenHandler>();
    })
    .Build();


using var scope = host.Services.CreateScope();

var mainMenu = scope.ServiceProvider.GetService<MainMenu>();

await mainMenu.StartAsync();