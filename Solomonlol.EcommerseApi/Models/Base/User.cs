using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solomonlol.EcommerseApi.Models.Base
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        public string Login { get; set; } = null!;
        [Required]
        public string PasswordHash { get; set; } = string.Empty!;
        [StringLength(maximumLength: 50, MinimumLength = 2)]
        public string FirstName { get; set; } = null!;
        [StringLength(maximumLength: 50, MinimumLength = 2)]
        public string LastName { get; set; } = null!;
        [EmailAddress]
        public string? Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;
        public ICollection<Sale> Sales { get; set; } = [];
    }
}
