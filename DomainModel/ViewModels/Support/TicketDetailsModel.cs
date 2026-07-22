using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.ViewModels.Support
{
    public class TicketDetailsModel
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

        [Display(Name = "وضعیت پاسخ")]
        public bool IsAnswered { get; set; }

        [Display(Name = "اولویت")]
        public byte Priority { get; set; }

        [Display(Name = "وضعیت تیکت")]
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
