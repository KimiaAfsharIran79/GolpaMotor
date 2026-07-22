using System.ComponentModel.DataAnnotations;

namespace DomainModel.ViewModels.Support
{
    public class TicketDetailsViewModel
    {
        public long SupportTicketID { get; set; }

        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "متن تیکت")]
        public string Message { get; set; }

        [Display(Name = "پاسخ پشتیبانی")]
        public string? Answer { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "تاریخ پاسخ")]
        public DateTime? AnswerDate { get; set; }

        public bool IsAnswered { get; set; }

        public byte Priority { get; set; }

        public bool IsClosed { get; set; }

        public string PriorityText =>
            Priority switch
            {
                1 => "کم",
                2 => "متوسط",
                3 => "زیاد",
                _ => "-"
            };
    }
}