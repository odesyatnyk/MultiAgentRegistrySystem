using AgentRegistry.Core.System.Entities;
using AgentRegistry.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace AgentRegistry.DataAccess.Context
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext() : base() { }

        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<Agent> Agents { get; set; }
        public DbSet<AgentCommand> AgentCommands { get; set; }
        public DbSet<AgentsCommunicationLog> AgentsCommunicationLogs { get; set; }
        public DbSet<AgentType> AgentTypes { get; set; }
        public DbSet<ExceptionLog> ExceptionLogs { get; set; }
        public DbSet<ScannerLog> ScannerLogs { get; set; }

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
