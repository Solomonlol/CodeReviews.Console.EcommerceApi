namespace EcommerseAPI.Frontend.Interfaces
{
    internal interface ISaleService : IPagedResultService
    {
        Task GetOne(int id, CancellationToken ct = default);
        Task Create(CancellationToken ct = default);
        Task Close(int id, CancellationToken ct = default);
    }
}
