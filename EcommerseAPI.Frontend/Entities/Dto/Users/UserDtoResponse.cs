using EcommerseAPI.Frontend.Entities.Dto.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EcommerseAPI.Frontend.Entities.Dto.Users
{
    public class UserDtoResponse
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Login is required.")]
        public string Login { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        [EmailAddress(ErrorMessage = "Can't validate email address. Please check is it correct.")]
        public string? Email { get; set; }
        [Phone(ErrorMessage = "Can't validate phone number. Please check is it correct.")]
        [Required(ErrorMessage = "Phone number is required.")]
        public string PhoneNumber { get; set; } = null!;
        public string Role { get; set; } = string.Empty!;
        public ICollection<SaleDtoResponse> Sales { get; set; } = [];
    }
}
