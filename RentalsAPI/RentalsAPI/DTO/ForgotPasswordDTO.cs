using System.ComponentModel.DataAnnotations;

namespace RentalsAPI.DTO
{
    public class ForgotPasswordDTO
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string NewPassword { get; set; } = string.Empty;

        [Required]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}

