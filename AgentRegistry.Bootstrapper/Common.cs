using AgentRegistry.Core.System.Repositories;
using AgentRegistry.DataAccess.Context;
using AgentRegistry.DataAccess.System.Repositories;
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

        internal static IExceptionLogRepository ExceptionLogRepository => (IExceptionLogRepository)ServiceProvider?.GetService(typeof(IExceptionLogRepository));

        public static ILogger Logger => (ILogger)ServiceProvider?.GetService(typeof(ILogger));

        public static void Bootstrap()
        {
            Config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            BuildServices();
        }

        internal static void BuildServices()
        {
            ServiceProvider = new ServiceCollection()
                .AddDbContext<IDataContext, DataContext>(
                options =>
                {
                    options.UseLazyLoadingProxies();
                    options.UseSqlServer(Config.GetConnectionString("AgentRegistryDatabase"));
                },
                ServiceLifetime.Scoped,
                ServiceLifetime.Scoped)
                .AddScoped<IExceptionLogRepository, ExceptionLogRepository>()
                .AddScoped<ILogger, Logger.Logger>()
                .BuildServiceProvider();
        }
    }
}
