using DomainModel.Models;
using DomainModel.ViewModels.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public interface ISupportRepository
    {        
        Task AddTicket(SupportTicket ticket);
        Task<List<TicketListItem>> GetUserTickets(string userId);

        Task<TicketDetailsModel?> GetTicketDetails(long ticketId, string userId);
        Task SaveChangesAsync();
    }
}
