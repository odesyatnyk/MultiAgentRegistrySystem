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

            while (true)
            {
                Console.WriteLine("wait input");
                if (Console.ReadKey().Key != ConsoleKey.Q)
                    AsynchronousClient.SendMessage(11001, "Hello");
            }
        }
    }
}
