using EcommerseAPI.Frontend.Services.Factory.Sort;

namespace EcommerseAPI.Frontend.Interfaces
{
    internal interface IPagedResultService
    {
        Task<HttpResponseMessage?> GetAll(object? filter = null, SortParams? sort = null, int page = 1, int pageSize = 5, CancellationToken ct = default);
    }
}
