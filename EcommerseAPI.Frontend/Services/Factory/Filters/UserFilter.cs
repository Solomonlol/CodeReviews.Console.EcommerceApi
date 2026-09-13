using EcommerseAPI.Frontend.Interfaces;
using Spectre.Console;
using System.Reflection;

namespace EcommerseAPI.Frontend.Services.Factory.Filters
{
    public class UserFilter : IFilter
    {
        public int? Id { get; set; } = null;
        public string? Login { get; set; } = null;
        public string? FirstName { get; set; } = null;
        public string? LastName { get; set; } = null;
        public string? Email { get; set; } = null;
        public string? PhoneNumber { get; set; } = null;
       
    }
}
