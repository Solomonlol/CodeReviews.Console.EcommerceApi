using EcommerseAPI.Frontend.Entities.Sort;

namespace EcommerseAPI.Frontend.Interfaces
{
    internal interface IPagedResultService
    {
        Task<HttpResponseMessage?> GetAll(IFilter? filter = null, SortParams? sort = null, int page = 1, int pageSize = 5, CancellationToken ct = default);

    }
}
