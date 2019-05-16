using AgentRegistry.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgentRegistry.ApplicationLogic.System
{
    public class SystemManager
    {
        private readonly IDataContext dataContext;

        public SystemManager(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
    }
}
