using AgentRegistry.Bootstrapper;
using AgentRegistry.Infrastructure.Common;
using System;
using System.Threading.Tasks;

namespace AgentRegistry.Scanner
{
    class Program
    {
        static void Main(string[] args)
        {
            Common.Bootstrap();

            try
            {
                var task = PortScanner.RunPortScanAsync(new System.Collections.Generic.List<int>() { 11001 });
                task.Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine("  error : {0}",
                    ex.InnerException != null
                        ? ex.InnerException.Message
                        : ex.Message);
            }
        }
    }
}
