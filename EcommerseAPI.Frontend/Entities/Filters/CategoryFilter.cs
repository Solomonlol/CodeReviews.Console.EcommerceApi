using EcommerseAPI.Frontend.Interfaces;

namespace EcommerseAPI.Frontend.Entities.Filters
{
    public class CategoryFilter : IFilter
    {
        public string? Name { get; set; } = null;
        public string? Description { get; set; } = null;
    }
}
