using Solomonlol.EcommerseApi.Models.Base;

namespace Solomonlol.EcommerseApi.Services.Extensions.Filters
{
    public class ProductFilter
    {
        public string? Name { get; set; }
        public decimal? MaxPrice { get; set; }
        public decimal? MinPrice { get; set; }
    }
}
