using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.ViewModels.Support
{
    public class TicketListItem
    {
        [Display(Name = "شناسه")]
        public long SupportTicketID { get; set; }

        [Display(Name = "عنوان تیکت")]
        public string Title { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "وضعیت پاسخ")]
        public bool IsAnswered { get; set; }
    }
}
