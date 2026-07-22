using System.ComponentModel.DataAnnotations;

namespace GolpaMotor.Models.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
