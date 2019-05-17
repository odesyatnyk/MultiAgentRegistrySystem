using System;
using System.Collections.Generic;
using System.Text;

namespace AgentRegistry.Infrastructure.Common
{
    public interface ILogger
    {
        void LogException(Exception ex);
    }
}
