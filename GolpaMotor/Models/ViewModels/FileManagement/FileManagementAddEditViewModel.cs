using System.ComponentModel.DataAnnotations;

namespace GolpaMotor.Models.ViewModels.FileManagement
{
    public class FileManagementAddEditViewModel
    {
        public int FileID { get; set; }
        public string UploadingUserID { get; set; }
        public DateTime UploadDate { get; set; }


        [Display(Name = "فایل")]
        [Required(ErrorMessage = "انتخاب فایل اجباری است.")]
        public IFormFile? UploadedFile { get; set; }

        
         [Display(Name = "ورژن کوچک عکس")]
        [Required(ErrorMessage = "انتخاب فایل اجباری است.")]
        public IFormFile? ThumbnailFile { get; set; }


        [Required(ErrorMessage = "شناسه نوع فایل اجباری است.")]
        [Range(1, int.MaxValue, ErrorMessage = "شناسه نوع فایل باید یک عدد مثبت باشد.")]
        [Display(Name = "شناسه نوع فایل")]
        public int FileTypeID { get; set; }



        [Range(1, long.MaxValue, ErrorMessage = "شناسه محصول باید یک عدد مثبت باشد.")]
        [Display(Name = "شناسه محصول")]
        public long? ProductID { get; set; }


        [StringLength(50, MinimumLength = 3, ErrorMessage = "نام جایگزین باید بین ۳ تا ۵۰ کاراکتر باشد.")]
        [Display(Name = "نام جایگزین")]
        public string? Alt { get; set; }



        [Range(0, int.MaxValue, ErrorMessage = "ترتیب نمایش باید یک عدد مثبت باشد.")]
        [Display(Name = "ترتیب نمایش")]
        public int? SortOrder { get; set; }


        [Required(ErrorMessage = "وضعیت باید مشخص باشد.")]
        [Display(Name = "پیش‌فرض")]
        public bool IsDefault { get; set; }
    }
}
