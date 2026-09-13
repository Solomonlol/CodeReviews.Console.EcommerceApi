using EcommerseAPI.Frontend.Interfaces;
using System.Reflection;

namespace EcommerseAPI.Frontend.Services.Factory.Filters
{
    public class SaleFilter : IFilter
    {
        public int? Id { get; set; } = null;
        public decimal? MaxTotalPrice { get; set; } = null;
        public decimal? MinTotalPrice { get; set; } = null;
        public string? CreatedAt { get; set; } = null;
        public string? EndedAt { get; set; } = null;

        
    }
}
