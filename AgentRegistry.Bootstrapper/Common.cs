using AgentRegistry.DataAccess.Context;
using AgentRegistry.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace AgentRegistry.Bootstrapper
{
    public class Common
    {
        public static IConfiguration Config { get; protected set; }

        public static IServiceProvider ServiceProvider { get; protected set; }

        public static IDataContext DataContext => (IDataContext)ServiceProvider?.GetService(typeof(IDataContext));

        public static void Bootstrap()
        {
            Config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            ServiceProvider = new ServiceCollection()
                .AddDbContext<IDataContext, DataContext>(
                options =>
                {
                    options.UseLazyLoadingProxies();
                    options.UseSqlServer(Config.GetConnectionString("AgentRegistryDatabase"));
                },
                ServiceLifetime.Scoped,
                ServiceLifetime.Scoped)
                .BuildServiceProvider();
        }
    }
}
