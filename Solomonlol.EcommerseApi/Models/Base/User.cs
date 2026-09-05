using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Solomonlol.EcommerseApi.Seeding.EnumSeedHelper;

namespace Solomonlol.EcommerseApi.Models.Base
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Login name length should be between 3 and 50 characters.")]
        public string Login { get; set; } = null!;
        [Required]
        public string PasswordHash { get; set; } = string.Empty!;
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "First name length should be between 2 and 50 characters.")]
        public string FirstName { get; set; } = null!;
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Last name length should be between 2 and 50 characters.")]
        public string LastName { get; set; } = null!;
        [EmailAddress]
        [Required]
        public string? Email { get; set; }
        [Required]
        public string Role { get; set; } = $"{RoleEnum.User}";
        [Phone]
        public string PhoneNumber { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;
        public ICollection<Sale> Sales { get; set; } = [];
    }
}
