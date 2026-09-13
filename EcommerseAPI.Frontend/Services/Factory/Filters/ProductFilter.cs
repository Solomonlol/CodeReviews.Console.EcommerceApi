using EcommerseAPI.Frontend.Interfaces;
using System.Reflection;

namespace EcommerseAPI.Frontend.Services.Factory.Filters
{
    public class ProductFilter : IFilter
    {
        public string? Name { get; set; } = null;
        public decimal? MaxPrice { get; set; } = null;
        public decimal? MinPrice { get; set; } = null;
        
        
    }
}
