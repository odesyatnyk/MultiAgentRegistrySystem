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
                var task = PortScanner.RunPortScanAsync(10999, 65535);
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
