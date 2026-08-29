using System.ComponentModel.DataAnnotations;

namespace Solomonlol.EcommerseApi.Models.Dto
{
    public class UserDtoCreation : UserDto
    {
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password minimum length is 8 characters and maximum length is 50")]
        public string Password { get; set; } = string.Empty;
    }
}
