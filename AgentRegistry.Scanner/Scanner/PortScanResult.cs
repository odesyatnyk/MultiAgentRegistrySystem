using System;
using System.Collections.Generic;
using System.Text;

namespace AgentRegistry.Scanner
{
    public class PortScanResult
    {
        public int PortNum { get; set; }
        public bool IsPortOpen { get; set; }
    }
}
