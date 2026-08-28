using Solomonlol.EcommerseApi.Interfaces;
using Solomonlol.EcommerseApi.Models.Dto;

namespace Solomonlol.EcommerseApi.Endpoints
{
    public static class CategoryEndpoint
    {
        public static void MapCategoryEndpoint(this WebApplication app)
        {
            //get all by page
            app.MapGet("api/v1/categories?page={page}", async (int page, ICategoryService service, CancellationToken ct) =>
            {
                var result = await service.GetAll(page, ct);
                return Results.Ok(result.Value);
            });
            //get one
            app.MapGet("api/v1/categories/{name}", async (string name, ICategoryService service, CancellationToken ct) =>
            {
                var result = await service.Get(name, ct);
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
            app.MapPut("api/v1/categories/{name}", async (string name, CategoryDto category, ICategoryService service, CancellationToken ct) =>
            {
                var result = await service.Update(name, category, ct);
                return result.IsSuccess 
                ? Results.Ok() 
                : Results.NotFound(result?.Error);
            });
            //delete
            app.MapDelete("api/v1/categories/{name}", async (string name, ICategoryService service, CancellationToken ct) =>
            {
                var result = await service.Delete(name, ct);
                return result.IsSuccess 
                ? Results.NoContent() 
                : Results.NotFound(result?.Error);
            });
        }
    }
}
