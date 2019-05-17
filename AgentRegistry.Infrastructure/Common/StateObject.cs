using System.Net.Sockets;
using System.Text;

namespace AgentRegistry.Infrastructure.Common
{
    public class StateObject
    {
        public const int BufferSize = 1024;

        public Socket WorkSocket { get; set; } = null;

        public byte[] Buffer { get; set; } = new byte[BufferSize];

        public StringBuilder StringBuilder { get; set; } = new StringBuilder();
    }
}