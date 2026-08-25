using Microsoft.EntityFrameworkCore;
using Solomonlol.EcommerseApi;
using Solomonlol.EcommerseApi.Models.Base;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MSSQLServer");

builder.Services.AddDbContext<ApplicationContext>(options =>
                            options.UseSqlServer(connectionString));
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.Run();
