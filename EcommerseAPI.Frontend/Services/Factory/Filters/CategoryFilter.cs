using EcommerseAPI.Frontend.Interfaces;
using System.Reflection;

namespace EcommerseAPI.Frontend.Services.Factory.Filters
{
    public class CategoryFilter : IFilter
    {
        public string? Name { get; set; } = null;
        public string? Description { get; set; } = null;

        
    }
}
