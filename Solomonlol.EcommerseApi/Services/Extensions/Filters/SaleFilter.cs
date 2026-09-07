namespace Solomonlol.EcommerseApi.Services.Extensions.Filters
{
    public class SaleFilter
    {
        public int? Id { get; set; }
        public decimal? MaxTotalPrice { get; set; }
        public decimal? MinTotalPrice { get; set; }
        public string? CreatedAt { get; set; }
        public string? EndedAt { get; set; }
    }
}
