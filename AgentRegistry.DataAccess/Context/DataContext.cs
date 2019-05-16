using AgentRegistry.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace AgentRegistry.DataAccess.Context
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext() : base() { }

        public DataContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=AgentRegistry_db;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
