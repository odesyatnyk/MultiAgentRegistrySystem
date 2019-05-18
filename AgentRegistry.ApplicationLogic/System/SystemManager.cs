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

        public List<AvailableAgentDTO> GetAvailableAgents()
        {
            var scanSession = dataContext.Set<ScannerLog>()
                .Where(x => x.IsSuccess ?? false)
                .OrderByDescending(x => x.DateTimeScanEnd)
                .First();

            return scanSession.Agents.Select(x => new AvailableAgentDTO
            {
                AgentId = x.Id.ToString(),
                AgentType = x.AgentType.AgentTypeName,
                IpAddress = x.IpAddress,
                Port = x.Port.ToString()
            }).OrderBy(x => x.AgentType).ToList();
        }

        public List<string> GetAgentCommands(string agentTypeName)
        {
            var agentType = dataContext.Set<AgentType>().Single(x => x.AgentTypeName == agentTypeName);

            return agentType.Commands.Select(x => x.CommandName).ToList();
        }

        public int CreateCommandToAgent(CommandToAgentDTO commandParams)
        {
            var scanSession = dataContext.Set<ScannerLog>()
                .Where(x => x.IsSuccess ?? false)
                .OrderByDescending(x => x.DateTimeScanEnd)
                .First();

            var agentFrom = scanSession.Agents.First(x => x.IpAddress == commandParams.Ip && x.Port == commandParams.FromPort);

            var agentTo = scanSession.Agents.First(x => x.IpAddress == commandParams.Ip && x.Port == commandParams.ToPort);

            var command = dataContext.Set<AgentCommand>().First(x => x.AgentType.Id == agentTo.AgentType.Id && x.CommandName == commandParams.CommandName);

            var agentCommunicationLog = new AgentsCommunicationLog
            {
                AgentFrom = agentFrom,
                AgentTo = agentTo,
                DateTimeCommunication = DateTime.Now,
                Command = command
            };

            dataContext.Add(agentCommunicationLog);
            dataContext.SaveChanges();

            return agentCommunicationLog.Id;
        }

        public void CompleteAgentCommand(int idCommand, string responseCode)
        {
            var communicationLog = dataContext.Set<AgentsCommunicationLog>().Single(x => x.Id == idCommand);

            var response = dataContext.Set<AgentCommandResponse>().Single(x => x.AgentCommand.Id == communicationLog.Command.Id && x.ResponseCode == responseCode);

            communicationLog.IsSuccess = true;
            communicationLog.CommandResponse = response;
            dataContext.SaveChanges();
        }
    }
}
