using AgentRegistry.Core.System.Entities;
using AgentRegistry.Core.System.Repositories;
using AgentRegistry.DataAccess.System.Repositories;
using AgentRegistry.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgentRegistry.ApplicationLogic.System
{
    public class SystemManager
    {
        private readonly IDataContext dataContext;

        private readonly IScannerLogRepository scannerLogRepository;

        public SystemManager(IDataContext dataContext)
        {
            this.dataContext = dataContext;
            this.scannerLogRepository = new ScannerLogRepository(this.dataContext);
        }

        public int StartScanSession()
        {
            var scanSession = new ScannerLog
            {
                DateTimeScanStart = DateTime.Now
            };

            scannerLogRepository.Add(scanSession);

            dataContext.SaveChanges();

            return scanSession.Id;
        }

        public void CompleteScanSession(int idScanSession, ScanResultDTO scanResult)
        {
            var scanSession = scannerLogRepository.FindById(idScanSession);

            if (scanResult.OpenPorts.Any())
            {
                foreach (var item in scanResult.OpenPorts)
                {
                    scanSession.Agents.Add(new Agent
                    {
                        AgentType = dataContext.Set<AgentType>().First(x => x.AgentTypeName == item.AgentType),
                        Port = item.Port,
                        IpAddress = scanResult.IpAddress,
                        ScannerLog = scanSession
                    });
                }
            }

            scanSession.DateTimeScanEnd = DateTime.Now;
            scanSession.IsSuccess = true;

            scannerLogRepository.Update(scanSession);
        }
    }
}
