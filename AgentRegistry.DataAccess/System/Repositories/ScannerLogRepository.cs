using AgentRegistry.Core.System.Entities;
using AgentRegistry.Core.System.Repositories;
using AgentRegistry.Infrastructure.Common;
using LampsCost.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgentRegistry.DataAccess.System.Repositories
{
    public class ScannerLogRepository : BaseRepository<ScannerLog>, IScannerLogRepository
    {
        public ScannerLogRepository(IDataContext dataContext) : base(dataContext) { }
    }
}
