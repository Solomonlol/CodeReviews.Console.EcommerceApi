using EcommerseAPI.Frontend.Entities.Sort;

namespace EcommerseAPI.Frontend.Interfaces
{
    internal interface IUrlService
    {
        Task<string> GetUrl(IFilter? filter = null, SortParams? sort = null, int? page = null, int? pageSize = null, CancellationToken ct = default);
    }
}
