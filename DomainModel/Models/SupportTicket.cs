using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public class SupportTicket
    {
        public long SupportTicketID { get; set; }

        public string UserID { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public string? Answer { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? AnswerDate { get; set; }

        public bool IsAnswered { get; set; }

        public byte Priority { get; set; } // 1=کم 2=متوسط 3=زیاد

        public bool IsClosed { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}

