namespace EcommerseAPI.Frontend.Interfaces
{
    internal interface IFilterFactory
    {
        Task Create<T>(T filter, CancellationToken ct = default);
    }
}
