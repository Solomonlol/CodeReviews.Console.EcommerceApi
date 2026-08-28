namespace Solomonlol.EcommerseApi.Models.Dto
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; } = [];
        public int Page {  get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
