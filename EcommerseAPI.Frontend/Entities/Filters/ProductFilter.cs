using EcommerseAPI.Frontend.Interfaces;

namespace EcommerseAPI.Frontend.Entities.Filters
{
    public class ProductFilter : IFilter
    {
        public string? Name { get; set; } = null;
        public decimal? MaxPrice { get; set; } = null;
        public decimal? MinPrice { get; set; } = null;
    }
}
