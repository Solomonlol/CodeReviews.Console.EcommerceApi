using Microsoft.AspNetCore.Authorization;
using Solomonlol.EcommerseApi.Interfaces;
using Solomonlol.EcommerseApi.Models.Dto.User;
using Solomonlol.EcommerseApi.Services.Extensions.Filters;
using Solomonlol.EcommerseApi.Services.Extensions.Sort;
using System.Security.Claims;

namespace Solomonlol.EcommerseApi.Endpoints
{
    public static class UserEndpoint
    {
        public static void MapUserEndpoints(this WebApplication app)
        {
            //get your account
            app.MapGet("api/v1/users/me", [Authorize] async (ClaimsPrincipal claims, IUserService service, CancellationToken ct) =>
            {
                var login = claims.FindFirst(ClaimTypes.Name)?.Value;
                if (string.IsNullOrEmpty(login) || string.IsNullOrWhiteSpace(login)) return Results.BadRequest();

                var result = await service.GetByLogin(login, ct);
                return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.NotFound();
            });
            //get one
            app.MapGet("api/v1/users/{login}", [Authorize(Roles = "Admin, Manager")] async (string login, IUserService service, CancellationToken ct) =>
            {
                if (string.IsNullOrEmpty(login) || string.IsNullOrWhiteSpace(login)) return Results.BadRequest();

                var result = await service.GetByLogin(login, ct);
                return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.NotFound();
            });
            //get all
            app.MapGet("api/v1/users", [Authorize(Roles = "Admin, Manager")] async ([AsParameters]UserFilter filter, [AsParameters] SortParams sortParams, IUserService service, CancellationToken ct, int page = 1, int pageSize = 5) =>
            {
                var result = await service.GetAll(filter, sortParams, page, pageSize, ct);
                return Results.Ok(result.Value);
            });
            //create
            app.MapPost("api/v1/users", [AllowAnonymous] async (UserDtoCreation item, IUserService service, CancellationToken ct) =>
            {
                var result = await service.Create(item, ct);
                return result.IsSuccess
                ? Results.Created($"api/v1/users/{item.Login}", item)
                : Results.Conflict(result.Error);
            });
            //update
            app.MapPut("api/v1/users/{login}", [Authorize(Roles = "Admin, Manager")] async (string login, UserDtoRequest item, IUserService service, CancellationToken ct) =>
            {
                var result = await service.Update(login, item, ct);
                return result.IsSuccess
                ? Results.Ok(item)
                : Results.BadRequest(result.Error);
            });
            //delete
            app.MapDelete("api/v1/users/{login}", [Authorize(Roles = "Admin, Manager")] async (string login, IUserService service, CancellationToken ct) =>
            {
                if (string.IsNullOrEmpty(login) || string.IsNullOrWhiteSpace(login))
                    return Results.BadRequest();
                var result = await service.Delete(login, ct);
                return result.IsSuccess ? Results.NoContent() : Results.NotFound(result.Error);
            });
        }
    }
}
