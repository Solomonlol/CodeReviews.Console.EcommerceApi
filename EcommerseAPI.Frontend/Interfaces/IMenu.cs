namespace EcommerseAPI.Frontend.Interfaces
{
    internal interface IMenu
    {
        Task StartAsync(CancellationToken ct = default);
    }
}
