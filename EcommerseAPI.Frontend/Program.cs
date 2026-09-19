using EcommerseAPI.Frontend.Handlers;
using EcommerseAPI.Frontend.Interfaces;
using EcommerseAPI.Frontend.Menus;
using EcommerseAPI.Frontend.Services;
using EcommerseAPI.Frontend.Services.Factory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {

        services.AddTransient<TokenHandler>();
        services.AddSingleton<ITokenService, TokenService>();
        services.AddSingleton<IUrlServiceFactory, UrlServiceFactory>();
        services.AddSingleton<IShoppingCartService, ShoppingCartService>();

        services.AddTransient<MainMenu>();
        services.AddTransient<CatalogMenu>();
        services.AddTransient<AccountMenu>();
        services.AddTransient<SaleMenu>();
        services.AddTransient<CategoryMenu>();


        services.AddScoped<ITableDrawingService, TableDrowingService>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IAttrributeService, AttributeService>();
        services.AddScoped<ISaleService, SaleService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IUrlService, UrlService>();
        services.AddScoped<ICategoryService, CategoryService>();
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