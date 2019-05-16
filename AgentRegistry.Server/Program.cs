

using AgentRegistry.Infrastructure.Common;
using System;

namespace AgentRegistry.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Startup.Bootstrap();
            var a = Startup.ServiceProvider.GetService(typeof(IDataContext));
            Console.ReadLine();
        }
    }
}
