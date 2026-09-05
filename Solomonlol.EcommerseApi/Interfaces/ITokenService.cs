namespace Solomonlol.EcommerseApi.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string userId, string userName, string role);
    }
}
