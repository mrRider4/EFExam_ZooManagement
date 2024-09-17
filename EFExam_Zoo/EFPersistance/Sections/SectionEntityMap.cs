using EFExam_Zoo.Models.Sections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFExam_Zoo.EFPersistance.Sections;

public class SectionEntityMap:IEntityTypeConfiguration<Section>
{
    public void Configure(EntityTypeBuilder<Section> builder)
    {
        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.Id).UseIdentityColumn();
        builder.Property(_ => _.Area).IsRequired();
        builder.Property(_ => _.AnimalsCount).IsRequired().HasDefaultValue(0);
        builder.Property(_ => _.Description).IsRequired().HasMaxLength(500);
        builder.HasOne(_ => _.Zoo).WithMany(_ => _.Sections)
            .HasForeignKey(_ => _.ZooId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(_ => _.Animal).WithMany(_ => _.Sections)
            .HasForeignKey(_ => _.AnimalId).OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(_ => _.Ticket).WithOne(_ => _.Section)
            .HasForeignKey<Section>(_ => _.TicketId).OnDelete(DeleteBehavior.SetNull);
    }
}