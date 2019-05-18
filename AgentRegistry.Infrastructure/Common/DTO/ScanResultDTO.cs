using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgentRegistry.Infrastructure.Common
{
    public class ScanResultDTO
    {
        public ScanResultDTO([NotNull] IEnumerable<OpenPortDTO> openPorts, [NotNull] IEnumerable<int> closedPorts, string ipAddress)
        {
            OpenPorts = new List<OpenPortDTO>(openPorts);
            ClosedPorts = new List<int>(closedPorts);
            IpAddress = ipAddress;
        }

        public IEnumerable<OpenPortDTO> OpenPorts { get; protected set; } = new List<OpenPortDTO>();

        public IEnumerable<int> ClosedPorts { get; protected set; } = new List<int>();

        public string IpAddress { get; protected set; }
    }
}
