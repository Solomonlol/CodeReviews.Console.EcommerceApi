using Solomonlol.EcommerseApi.Interfaces;
using Solomonlol.EcommerseApi.Models.Dto.Product;
using System.Runtime.CompilerServices;

namespace Solomonlol.EcommerseApi.Endpoints
{
    public static class ProductEndpoints
    {
        public static void MapProductEndpoints(this WebApplication app)
        {
            //get all by page
            app.MapGet("api/v1/products", async (IProductService service, CancellationToken ct, int page = 1, int pageSize = 5) =>
            {
                page=Math.Max(page, 1);
                pageSize = Math.Clamp(pageSize, 1, 30);
                var result = await service.GetAll(page, pageSize, ct);
                return Results.Ok(result.Value);
            });
            //get one
            app.MapGet("api/v1/products/{productName}", async (string productName, IProductService service, CancellationToken ct) =>
            {
                var result = await service.Get(productName, ct);
                return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound();
            });
            //create
            app.MapPost("api/v1/products", async (ProductDto item, IProductService service, CancellationToken ct) =>
            {
                var result = await service.Create(item, ct);
                return result.IsSuccess 
                    ? Results.Created($"api/v1/products/{item.Name}", item) 
                    : Results.Conflict(result.Error);
            });
            //update
            app.MapPut("api/v1/products/{productName}", async (string productName, ProductDto item, IProductService service, CancellationToken ct) =>
            {
                var result = await service.Update(productName, item, ct);
                return result.IsSuccess ? Results.Ok(item) : Results.NotFound();
            });
            //delete
            app.MapDelete("api/v1/products/{productName}", async (string productName, IProductService service, CancellationToken ct) =>
            {
                var result = await service.Delete(productName, ct);
                return result.IsSuccess 
                    ? Results.NoContent() 
                    : Results.NotFound(result.Error);
            });

            //add attribute value
            app.MapPost("api/v1/products/{productName}/attributes", async (string productName, ProductAttributeValueDto attribute, IAttributeValueService service, CancellationToken ct) =>
            {
                var result = await service.AddAttributeValue(productName, attribute, ct);
                return result.IsSuccess
                ? Results.Created($"api/v1/categories/{productName}/attributes", attribute)
                : Results.Conflict(result?.Error);
            });
            //update attribute value
            app.MapPut("api/v1/products/{productName}/attributes", async (string productName, string attributeName, ProductAttributeValueDto attribute, IAttributeValueService service, CancellationToken ct) =>
            {
                var result = await service.UpdateAttributeValue(productName, attributeName, attribute, ct);
                return result.IsSuccess
                ? Results.Ok(attribute)
                : Results.Conflict(result?.Error);
            });
        }
    }
}
