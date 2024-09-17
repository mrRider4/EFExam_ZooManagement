using EFExam_Zoo.Models.Zoos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFExam_Zoo.EFPersistance.Zoos;

public class ZooEntityMap:IEntityTypeConfiguration<Zoo>
{
    public void Configure(EntityTypeBuilder<Zoo> builder)
    {
        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.Id).UseIdentityColumn();
        builder.Property(_ => _.Name).IsRequired().HasMaxLength(100);
        builder.Property(_ => _.Address).IsRequired().HasMaxLength(100);
    }
}