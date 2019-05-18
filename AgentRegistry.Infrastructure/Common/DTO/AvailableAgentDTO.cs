using System;
using System.Collections.Generic;
using System.Text;

namespace AgentRegistry.Infrastructure.Common
{
    public class AvailableAgentDTO
    {
        public string AgentId { get; set; }

        public string AgentType { get; set; }

        public string IpAddress { get; set; }

        public string Port { get; set; }
    }
}
