using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Zamin.Extensions.Events.Outbox.Dal.EF;

namespace APIService.Infra.Data.Sql.Commands.Common
{
    public class APIServiceDbContextCommandDbContext : BaseOutboxCommandDbContext
    {
        public APIServiceDbContextCommandDbContext(DbContextOptions<APIServiceDbContextCommandDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}