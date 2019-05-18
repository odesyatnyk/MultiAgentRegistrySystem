using AgentRegistry.Bootstrapper;
using AgentRegistry.Client;
using AgentRegistry.Server.Listener;
using System;
using System.Threading.Tasks;

namespace AgentRegistry.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Common.Bootstrap();

            Task.Run(() => AsynchronousSocketListener.StartListening());

            Console.ReadLine();
        }
    }
}
