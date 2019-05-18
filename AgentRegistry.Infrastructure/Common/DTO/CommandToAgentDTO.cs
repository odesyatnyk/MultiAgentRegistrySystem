using System;
using System.Collections.Generic;
using System.Text;

namespace AgentRegistry.Infrastructure.Common
{
    public class CommandToAgentDTO
    {
        public string Ip { get; set; }

        public int ToPort { get; set; }

        public string CommandName { get; set; }

        public int FromPort { get; set; }
    }
}
