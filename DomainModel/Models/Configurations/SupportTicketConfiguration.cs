using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models.Configurations
{
    public class SupportTicketConfiguration : IEntityTypeConfiguration<SupportTicket>
    {
        public void Configure(EntityTypeBuilder<SupportTicket> builder)
        {
            builder.HasKey(x => x.SupportTicketID);

            builder.Property(x => x.Title).IsRequired().HasMaxLength(200);

            builder.Property(x => x.Message).IsRequired().HasMaxLength(3000);

            builder.Property(x => x.Answer).HasMaxLength(3000);

            builder.Property(x => x.CreateDate).IsRequired();

            builder.Property(x => x.IsAnswered).HasDefaultValue(false);

            // User 1 ---> N Tickets
            builder.HasOne(x => x.User).WithMany(x => x.SupportTickets)
                .HasForeignKey(x => x.UserID).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
