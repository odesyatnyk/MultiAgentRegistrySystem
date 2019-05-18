using AgentRegistry.ApplicationLogic.System;
using AgentRegistry.Bootstrapper;
using AgentRegistry.Infrastructure.Common;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AgentRegistry.Scanner
{
    class Program
    {
        static void Main(string[] args)
        {
            Common.Bootstrap();

            int idScanSession = 0;

            Common.DataContext.TryBeginCommitTransaction(() =>
            {
                idScanSession = new SystemManager(Common.DataContext).StartScanSession();
            });

            ScanResultDTO scanResult = null;

            SystemHelper.TryCatchDefault(() =>
            {
                var task = PortScanner.RunPortScanAsync(Enumerable.Range(11001, 5));
                task.Wait();
                scanResult = task.Result;
            });

            Common.DataContext.TryBeginCommitTransaction(() =>
            {
                new SystemManager(Common.DataContext).CompleteScanSession(idScanSession, scanResult);
            });

            Console.ReadKey();
        }
    }
}
