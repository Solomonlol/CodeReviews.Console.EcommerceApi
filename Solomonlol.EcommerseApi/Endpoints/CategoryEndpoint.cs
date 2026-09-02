using Solomonlol.EcommerseApi.Interfaces;
using Solomonlol.EcommerseApi.Models.Dto.Category;
using Solomonlol.EcommerseApi.Models.Dto.Product;

namespace Solomonlol.EcommerseApi.Endpoints
{
    public static class CategoryEndpoint
    {
        public static void MapCategoryEndpoint(this WebApplication app)
        {
            //get all by page
            app.MapGet("api/v1/categories", async (ICategoryService service, CancellationToken ct, int page = 1, int pageSize = 5) =>
            {
                page = Math.Max(page, 1);
                pageSize = Math.Clamp(pageSize, 1, 30);
                var result = await service.GetAll(page, pageSize, ct);
                return Results.Ok(result.Value);
            });
            //get one
            app.MapGet("api/v1/categories/{categoryName}", async (string categoryName, ICategoryService service, CancellationToken ct) =>
            {
                var result = await service.Get(categoryName, ct);
                return result.IsSuccess 
                ? Results.Ok(result.Value) 
                : Results.NotFound(result?.Error);
            });
            //create
            app.MapPost("api/v1/categories", async (CategoryDto category, ICategoryService service, CancellationToken ct) =>
            {
                var result = await service.Create(category, ct);
                return result.IsSuccess 
                ? Results.Created()
                : Results.Conflict(result?.Error);
            });
            //update
            app.MapPut("api/v1/categories/{categoryName}", async (string categoryName, CategoryDto category, ICategoryService service, CancellationToken ct) =>
            {
                var result = await service.Update(categoryName, category, ct);
                return result.IsSuccess 
                ? Results.Ok() 
                : Results.NotFound(result?.Error);
            });
            //delete
            app.MapDelete("api/v1/categories/{categoryName}", async (string categoryName, ICategoryService service, CancellationToken ct) =>
            {
                var result = await service.Delete(categoryName, ct);
                return result.IsSuccess 
                ? Results.NoContent() 
                : Results.NotFound(result?.Error);
            });
            //add attribute
            app.MapPost("api/v1/categories/{categoryName}/attributes", async (string categoryName, ProductAttributeDto attribute, IAttributeService service, CancellationToken ct) =>
            {
                var result = await service.AddAttribute(categoryName, attribute, ct);
                return result.IsSuccess
                ? Results.Created($"api/v1/categories/{categoryName}/attributes/{attribute.Name}", result.Value)
                : Results.Conflict(result?.Error);
            });
            //update attribute
            app.MapPut("api/v1/categories/{categoryName}/attributes", async (string categoryName, string attributeName, ProductAttributeDto attribute, IAttributeService service, CancellationToken ct) =>
            {
                var result = await service.UpdateAttribute(categoryName, attributeName, attribute, ct);
                return result.IsSuccess
                ? Results.Ok()
                : Results.Conflict(result?.Error);
            });
            //delete attribute
            app.MapDelete("api/v1/categories/{categoryName}/attributes", async (string categoryName, string attributeName, IAttributeService service, CancellationToken ct) =>
            {
                var result = await service.DeleteAttribute(categoryName, attributeName, ct);
                return result.IsSuccess
                ? Results.NoContent()
                : Results.NotFound();
            });
        }
    }
}
