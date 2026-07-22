using System.ComponentModel.DataAnnotations;

namespace GolpaMotor.Models.ViewModels.Account
{
    public class LoginWith2faViewModel
    {
        [Required]
        [Display(Name = "Authenticator code")]
        public string TwoFactorCode { get; set; } = string.Empty;

        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
