using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Solomonlol.EcommerseApi;
using Solomonlol.EcommerseApi.Endpoints;
using Solomonlol.EcommerseApi.Interfaces;
using Solomonlol.EcommerseApi.Mapping;
using Solomonlol.EcommerseApi.Models.Base;
using Solomonlol.EcommerseApi.Seeding;
using Solomonlol.EcommerseApi.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MSSQLServer");

builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddDbContext<ApplicationContext>(options =>
                            options.UseSqlServer(connectionString));
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title="EcommerceApi",
        Version="v1"
    });
});
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAttributeService, AttributeService>();
builder.Services.AddScoped<IAttributeValueService, AttributeService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();

app.MapCategoryEndpoint();
app.MapProductEndpoints();
app.MapUserEndpoints();


await app.SeedAll();

await app.RunAsync();
