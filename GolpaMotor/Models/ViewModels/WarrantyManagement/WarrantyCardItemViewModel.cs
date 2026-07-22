using System.ComponentModel.DataAnnotations;

namespace GolpaMotor.Models.ViewModels.WarrantyManagement
{
    public class WarrantyCardItemViewModel
    {
        [RegularExpression(@"^\d{12}$",ErrorMessage = "شماره سریال باید 12 رقم باشد.")]
        [Required(ErrorMessage = "شماره سریال اجباری است.")]
        [Display(Name = "شماره سریال")]
        public string SerialNumber { get; set; } = "";

        [RegularExpression(@"^\d{12}$", ErrorMessage = "کد گارانتی باید 12 رقم باشد.")]
        [Required(ErrorMessage = "کد گارانتی اجباری است.")]
        [Display(Name = "کد گارانتی")]
        public string ScratchedCode { get; set; } = "";
    }
}
