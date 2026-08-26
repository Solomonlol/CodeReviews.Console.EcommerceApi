using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Solomonlol.EcommerseApi;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MSSQLServer");

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

var app = builder.Build();

app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
