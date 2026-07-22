using DomainModel.Models;
using System.ComponentModel.DataAnnotations;

namespace GolpaMotor.Models.ViewModels.WarrantyManagement
{
    public class RegisterationCardViewModel
    {
        public List<WarrantyCardItemViewModel> Cards { get; set; } = new()
        {
            new WarrantyCardItemViewModel()
        };

        [RegularExpression(@"^09\d{9}$", ErrorMessage = "شماره موبایل معتبر نیست.")]
        [Required(ErrorMessage = "شماره موبایل اجباری است.")]
        [Display(Name = "شماره موبایل")]
        public string CustomerPhoneNumber { get; set; }

        [StringLength(50)]
        public string? FirstName { get; set; }
        [StringLength(50)]
        public string? LastName { get; set; }
        
        // نقش‌های انتخاب شده
        public List<int> CustomerTypeIds { get; set; } = new();

        // لیست نقش‌ها برای نمایش
        public List<CustomerType> CustomerTypes { get; set; } = new();
    }
}