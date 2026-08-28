using Solomonlol.EcommerseApi.Interfaces;
using Solomonlol.EcommerseApi.Models.Dto;
using System.Runtime.CompilerServices;

namespace Solomonlol.EcommerseApi.Endpoints
{
    public static class ProductEndpoints
    {
        public static void MapProductEndpoints(this WebApplication app)
        {
            //get all by page
            app.MapGet("api/v1/products/{page}", async (int page, IProductService service, CancellationToken ct) =>
            {
                var result = await service.GetAll(page, ct);
                return Results.Ok(result.Value);
            });
            //get one
            app.MapGet("api/v1/product/{name}", async (string name, IProductService service, CancellationToken ct) =>
            {
                var result = await service.Get(name, ct);
                return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound();
            });
            //create
            app.MapPost("api/v1/products", async (ProductDto item, IProductService service, CancellationToken ct) =>
            {
                var result = await service.Create(item, ct);
                return result.IsSuccess 
                    ? Results.Created() 
                    : Results.Conflict(result.Error);
            });
            //update
            app.MapPut("api/v1/product/{name}", async (string name, ProductDto item, IProductService service, CancellationToken ct) =>
            {
                var result = await service.Update(name, item, ct);
                return result.IsSuccess ? Results.Ok() : Results.NotFound();
            });

            app.MapDelete("api/v1/product/{name}", async (string name, IProductService service, CancellationToken ct) =>
            {
                var result = await service.Delete(name, ct);
                return result.IsSuccess 
                    ? Results.NoContent() 
                    : Results.NotFound(result.Error);
            });
        }
    }
}
