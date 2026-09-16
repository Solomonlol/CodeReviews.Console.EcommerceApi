using EcommerseAPI.Frontend.Entities.Dto;

namespace EcommerseAPI.Frontend.Interfaces
{
    internal interface ITableDrawingService
    {
        Task DrowSimpleTable<T>(PagedResult<T>? pagedResult = null, string? title = null, CancellationToken ct = default, IEnumerable<T>? enumerableValues = null, bool isNestedDrawing = false);
    }
}
