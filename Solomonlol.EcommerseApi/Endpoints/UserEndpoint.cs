using Solomonlol.EcommerseApi.Interfaces;
using Solomonlol.EcommerseApi.Models.Dto.User;

namespace Solomonlol.EcommerseApi.Endpoints
{
    public static class UserEndpoint
    {
        public static void MapUserEndpoints(this WebApplication app)
        {
            //get one
            app.MapGet("api/v1/users/{login}", async (string login, IUserService service, CancellationToken ct) =>
            {
                var result = await service.GetByLogin(login, ct);
                return result.IsSuccess 
                ? Results.Ok(result.Value) 
                : Results.NotFound();
            });
            //get all
            app.MapGet("api/v1/users", async (IUserService service, CancellationToken ct, int page = 1, int pageSize = 5) =>
            {
                var result = await service.GetAll(page, pageSize, ct);
                return Results.Ok(result.Value);
            });
            //create
            app.MapPost("api/v1/users", async (UserDtoCreation item, IUserService service, CancellationToken ct) =>
            {
                var result = await service.Create(item, ct);
                return result.IsSuccess 
                ? Results.Created($"api/v1/users/{item.Login}", item) 
                : Results.Conflict(result.Error);
            });
            //update
            app.MapPut("api/v1/users/{login}", async (string login, string password, UserDtoRequest item, IUserService service, CancellationToken ct) =>
            {
                var result = await service.Update(login, password, item, ct);
                return result.IsSuccess 
                ? Results.Ok(item) 
                : Results.BadRequest(result.Error);
            });
            //delete
            app.MapDelete("api/v1/users/{login}", async (string login, string password, IUserService service, CancellationToken ct) =>
            {
                var result = await service.Delete(login, password, ct);
                return result.IsSuccess ? Results.NoContent() : Results.NotFound(result.Error);
            });
        }
    }
}
