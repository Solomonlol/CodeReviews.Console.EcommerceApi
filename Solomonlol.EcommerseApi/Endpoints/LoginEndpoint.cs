using Microsoft.AspNetCore.Identity.Data;
using Solomonlol.EcommerseApi.Interfaces;
using System.Runtime.CompilerServices;

namespace Solomonlol.EcommerseApi.Endpoints
{
    public static class LoginEndpoint
    {
        public static void MapLoginEndponts(this WebApplication app)
        {
            app.MapPost("/api/v1/login", async (LoginRequest request, ITokenService tokenService, IUserService userService, CancellationToken ct) =>
            {
                var result = await  userService.VerifyByEmail(request.Email, request.Password, ct);
                if (result.IsSuccess)
                {
                    var user = result.Value;
                    var token = tokenService.GenerateToken(user.Id.ToString(), user.Login, user.Role);
                    return Results.Ok(token);
                }
                else return Results.Unauthorized();
                //return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
            });
        }
    }
}
