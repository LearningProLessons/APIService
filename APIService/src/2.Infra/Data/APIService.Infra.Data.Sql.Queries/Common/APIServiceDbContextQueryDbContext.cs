using Microsoft.EntityFrameworkCore;
using Zamin.Infra.Data.Sql.Queries;

namespace APIService.Infra.Data.Sql.Queries.Common
{
    public class APIServiceDbContextQueryDbContext : BaseQueryDbContext
    {
        public APIServiceDbContextQueryDbContext(DbContextOptions<APIServiceDbContextQueryDbContext> options) : base(options)
        {
        }
    }
}