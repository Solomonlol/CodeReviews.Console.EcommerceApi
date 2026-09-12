using Spectre.Console;

namespace EcommerseAPI.Frontend.Services.Factory.Filters
{
    public class UserFilter
    {
        public int? Id { get; set; } = null;
        public string? Login { get; set; } = null;
        public string? FirstName { get; set; } = null;
        public string? LastName { get; set; } = null;
        public string? Email { get; set; } = null;
        public string? PhoneNumber { get; set; } = null;
    }
}
