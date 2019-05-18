using AgentRegistry.ApplicationLogic.System;
using AgentRegistry.Bootstrapper;
using AgentRegistry.Client;
using AgentRegistry.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace AgentRegistry.Server.Listener
{
    public class CommunicationManager
    {
        public class AvailableCommands
        {
            public const string Commands = "commands";

            public const string AgentsOnline = "agents_online";

            public const string AgentCommand = "agent_commands";

            public const string SendCommandTo = "send_command_to";

        }

        public static ReadOnlyCollection<string> AllCommands =>
            new ReadOnlyCollection<string>(new List<string>
            {
                AvailableCommands.Commands,
                AvailableCommands.AgentsOnline,
                AvailableCommands.AgentCommand,
                AvailableCommands.SendCommandTo
            });

        public static string HandleCommand(string command)
        {
            if (!AllCommands.Any(x => x == command.Split(" ").First()))
                return "Unknown_Command";

            if (command.StartsWith(AvailableCommands.Commands))
            {
                return HandleCommands();
            }
            else if (command.StartsWith(AvailableCommands.AgentsOnline))
            {
                return HandleAgentsOnline();
            }
            else if (command.StartsWith(AvailableCommands.AgentCommand))
            {
                return HandleAgentCommand(command);
            }
            else if (command.StartsWith(AvailableCommands.SendCommandTo))
            {
                return HandleSendCommandTo(command);
            }
            else
            {
                return "Unknown_Command";
            }
        }

        private static string HandleCommands()
            => $"Available commands:{Environment.NewLine}{string.Join(Environment.NewLine, AllCommands)}";

        private static string HandleAgentsOnline()
        {
            var availableAgents = new List<AvailableAgentDTO>();

            availableAgents = new SystemManager(Common.DataContext).GetAvailableAgents();

            if (!availableAgents.Any())
                return "No agents available";

            return availableAgents.Aggregate($"{Environment.NewLine}Available agents:{Environment.NewLine}", (result, agent) => result + $"Agent Id - {agent.AgentId}; Agent Type - {agent.AgentType}; IP Address - {agent.IpAddress}; Port - {agent.Port}{Environment.NewLine}");
        }

        private static string HandleAgentCommand(string command)
        {
            var agentType = command.Split(" ").Last();
            var agentCommands = new SystemManager(Common.DataContext).GetAgentCommands(agentType);
            return agentCommands.Aggregate($"{Environment.NewLine}Available {agentType} agent commands:{Environment.NewLine}", (result, agentCommand) => result + $"Command - {agentCommand};{Environment.NewLine}");
        }

        private static string HandleSendCommandTo(string command)
        {
            var commandParams = TranslateCommandToAgent(command);

            var idCommand = new SystemManager(Common.DataContext).CreateCommandToAgent(commandParams);
            var responseCode = new AsynchronousClient().SendMessage(commandParams.ToPort, commandParams.CommandName).Replace("<EOF>", string.Empty);

            new SystemManager(Common.DataContext).CompleteAgentCommand(idCommand, responseCode);

            return responseCode;
        }

        private static CommandToAgentDTO TranslateCommandToAgent(string command)
        {
            var commandParams = command.Split(" ");

            return new CommandToAgentDTO
            {
                Ip = commandParams[1],
                ToPort = Convert.ToInt32(commandParams[2]),
                CommandName = commandParams[3],
                FromPort = Convert.ToInt32(commandParams[4])
            };
        }
    }
}
