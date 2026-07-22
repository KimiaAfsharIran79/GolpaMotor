namespace GolpaMotor.Models.ViewModels.Support
{
    public class TicketListItemViewModel
    {
        public long TicketID { get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsAnswered { get; set; }
    }
}
