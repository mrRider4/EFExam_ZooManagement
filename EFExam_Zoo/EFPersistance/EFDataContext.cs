using EFExam_Zoo.EFPersistance.Zoos;
using Microsoft.EntityFrameworkCore;

namespace EFExam_Zoo.EFPersistance;

public class EFDataContext: DbContext
{
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer
        (
            "Data Source=Cloud4;Initial Catalog=DB_Zoo;" +
            "User Id=CLOUD4;Password=21102110;TrustServerCertificate=true"
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ZooEntityMap).Assembly);
    }
}