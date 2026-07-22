using System.ComponentModel.DataAnnotations;

namespace GolpaMotor.Models.ViewModels.Support
{
    public class TicketCreateViewModel
    {
        [Required]
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "متن پیام")]
        public string Message { get; set; }
    }
}
