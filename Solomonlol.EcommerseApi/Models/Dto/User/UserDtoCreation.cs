using System.ComponentModel.DataAnnotations;

namespace Solomonlol.EcommerseApi.Models.Dto.User
{
    public class UserDtoCreation : UserDtoRequest
    {
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password minimum length is 8 characters and maximum length is 50")]
        public string Password { get; set; } = string.Empty;
    }
}
