using AgentRegistry.Bootstrapper;
using AgentRegistry.Server.Listener;

namespace AgentRegistry.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Common.Bootstrap();

            AsynchronousSocketListener.StartListening();
        }
    }
}
