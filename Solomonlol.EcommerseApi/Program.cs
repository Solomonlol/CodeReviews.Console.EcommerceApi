using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Solomonlol.EcommerseApi;
using Solomonlol.EcommerseApi.Auth;
using Solomonlol.EcommerseApi.Endpoints;
using Solomonlol.EcommerseApi.Interfaces;
using Solomonlol.EcommerseApi.Mapping;
using Solomonlol.EcommerseApi.Models.Base;
using Solomonlol.EcommerseApi.Seeding;
using Solomonlol.EcommerseApi.Services;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MSSQLServer");

builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience= true,
            ValidAudience=AuthOptions.AUDIENCE,
            ValidateLifetime=true,
            IssuerSigningKey=AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true
        };
    });

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
builder.Services.AddScoped<ISaleService, SaleService>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();

app.MapCategoryEndpoint();
app.MapProductEndpoints();
app.MapUserEndpoints();
app.MapSaleEndpoints();


await app.SeedAll();

await app.RunAsync();
