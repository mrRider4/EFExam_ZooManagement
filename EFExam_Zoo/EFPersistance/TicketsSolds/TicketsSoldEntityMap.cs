using EFExam_Zoo.Models.TicketsSolds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFExam_Zoo.EFPersistance.TicketsSolds;

public class TicketsSoldEntityMap:IEntityTypeConfiguration<TicketsSold>
{
    public void Configure(EntityTypeBuilder<TicketsSold> builder)
    {
        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.Id).UseIdentityColumn();
        builder.Property(_ => _.Count).IsRequired();
        builder.Property(_ => _.TicketId).IsRequired();
    }
}