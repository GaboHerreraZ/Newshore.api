using Microsoft.EntityFrameworkCore;
using Newshore.api.Model.External;
using System.Xml;

namespace Newshore.api.Context
{
    public class RouteDb: DbContext
    {
        public RouteDb(DbContextOptions<RouteDb> options): base(options) { }

        public DbSet<RouteDto> Routes => Set<RouteDto>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RouteDto>()
                .HasKey(e => e.Id);
        }
    }
}
