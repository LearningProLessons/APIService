using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace APIService.Infra.Data.Sql.Commands.Common
{
    public class APIServiceDbContextCommandDbContextFactory : IDesignTimeDbContextFactory<APIServiceDbContextCommandDbContext>
    {
        public APIServiceDbContextCommandDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<APIServiceDbContextCommandDbContext>();

            builder.UseSqlServer("Server =.; Database=APIServiceDbContextDb;User Id = ;Password = ; MultipleActiveResultSets = true; Encrypt = false");

            return new APIServiceDbContextCommandDbContext(builder.Options);
        }
    }
}