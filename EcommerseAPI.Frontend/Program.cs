using EcommerseAPI.Frontend.Interfaces;
using EcommerseAPI.Frontend.Menus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        
        services.AddTransient<MainMenu>();
        services.AddTransient<ProductMenu>();
        services.AddHttpClient("ApiClient", (sp, client) =>
        {
            var config = sp.GetRequiredService<IConfiguration>();
            var baseUrl = config["ApiSettings:BaseUrl"] ?? "http://localhost:5254/";
            client.BaseAddress = new Uri(baseUrl);
        });
    })
    .Build();


using var scope = host.Services.CreateScope();

var mainMenu = scope.ServiceProvider.GetService<MainMenu>();

await mainMenu.StartAsync();