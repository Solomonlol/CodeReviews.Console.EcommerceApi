using Solomonlol.EcommerseApi.Interfaces;
using Solomonlol.EcommerseApi.Models.Base;
using Solomonlol.EcommerseApi.Models.Dto.Sale;

namespace Solomonlol.EcommerseApi.Endpoints
{
    public static class SaleEndpoints
    {
        public static void MapSaleEndpoints(this WebApplication app)
        {
            //get one
            app.MapGet("api/v1/sales/{saleId}", async (int saleId, ISaleService service, CancellationToken ct) =>
            {
                var result = await service.Get(saleId, ct);
                return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound(result.Error);
            });
            //get all by user
            app.MapGet("api/v1/{login}/sales", async (string login, ISaleService service, CancellationToken ct, int page = 1, int pageSize = 5) =>
            {
                page = Math.Max(page, 1);
                pageSize = Math.Clamp(pageSize, 1, 30);
                var result = await service.GetAllByLogin(login, page, pageSize, ct);
                return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound(result.Error);
            });
            //get all
            app.MapGet("api/v1/sales", async (ISaleService service, CancellationToken ct, int page = 1, int pageSize = 5) =>
            {
                page = Math.Max(page, 1);
                pageSize = Math.Clamp(pageSize, 1, 30);
                var result = await service.GetAll(page, pageSize, ct);
                return result.IsSuccess ? Results.Ok(result.Value) : Results.Problem(result.Error);
            });
            //delete
            app.MapDelete("api/v1/sales/{saleId}", async (int saleId, ISaleService service, CancellationToken ct) =>
            {
                var result = await service.CloseSale(saleId, ct);
                return result.IsSuccess ? Results.NoContent() : Results.NotFound(result.Error);
            });
            //create
            app.MapPost("api/v1/sales", async (SaleDtoRequest item, ISaleService service, CancellationToken ct) =>
            {
                var result = await service.Create(item, ct);
                return result.IsSuccess ? Results.Created($"api/v1/sales/{result?.Value?.Id}", result?.Value) : Results.Conflict(result.Error);
            });
        }
    }
}
