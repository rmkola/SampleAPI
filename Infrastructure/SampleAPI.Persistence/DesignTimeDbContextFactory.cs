using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SampleAPI.Persistence.Contexts;

namespace SampleAPI.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SampleAPIDbContext>
    {
        public SampleAPIDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<SampleAPIDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseSqlServer(Configuration.ConnectionString);
            return new(dbContextOptionsBuilder.Options);
        }
    }
}
