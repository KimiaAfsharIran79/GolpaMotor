using DataAccess.Services;
using DomainModel.Models;
using DomainModel.ViewModels.Support;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class SupportRepository : ISupportRepository
    {
        private readonly GolpaMotorDbContext db;
        public SupportRepository(GolpaMotorDbContext db)
        {
            this.db = db;
        }

        public async Task AddTicket(SupportTicket ticket)
        {
            await db.SupportTickets.AddAsync(ticket);
        }

        public async Task<TicketDetailsModel?> GetTicketDetails(long ticketId, string userId)
        {
            return await db.SupportTickets

                .Where(x => x.SupportTicketID == ticketId &&
                            x.UserID == userId)

                .Select(x => new TicketDetailsModel
                {
                    SupportTicketID = x.SupportTicketID,
                    Title = x.Title,
                    Message = x.Message,
                    Answer = x.Answer,
                    CreateDate = x.CreateDate,
                    AnswerDate = x.AnswerDate,
                    IsAnswered = x.IsAnswered,
                    Priority = x.Priority,
                    IsClosed = x.IsClosed
                })

                .FirstOrDefaultAsync();
        }

        public async Task<List<TicketListItem>> GetUserTickets(string userId)
        {
            return await db.SupportTickets.Where(x => x.UserID == userId)
                .OrderByDescending(x => x.CreateDate)
                .Select(x => new TicketListItem
                {
                    SupportTicketID = x.SupportTicketID,
                    Title = x.Title,
                    CreateDate = x.CreateDate,
                    IsAnswered = x.IsAnswered
                })
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await db.SaveChangesAsync();
        }      
    }
}
