using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AgentRegistry.Scanner
{
    public class PortScanner
    {
        private const int MinPort = 1;
        private const int MaxPort = 65535;
        private readonly IEnumerable<int> _portsToScan = new List<int>();
        private List<int> _openPorts = new List<int>();
        private List<int> _closedPorts = new List<int>();

        public ReadOnlyCollection<int> OpenPorts => new ReadOnlyCollection<int>(_openPorts);

        public ReadOnlyCollection<int> ClosedPorts => new ReadOnlyCollection<int>(_closedPorts);

        public static string Host { get; } = Dns.GetHostEntry(Dns.GetHostName()).AddressList.First(x => x.AddressFamily == AddressFamily.InterNetwork).ToString();

        public PortScanner(int minPort, int maxPort)
        {
            if (minPort > maxPort)
                throw new ArgumentException("Min port cannot be greater than max port");

            if (minPort < MinPort || minPort > MaxPort)
                throw new ArgumentOutOfRangeException($"Min port cannot be less than {MinPort} or greater than {MaxPort}");

            if (maxPort < MinPort || maxPort > MaxPort)
                throw new ArgumentOutOfRangeException($"Max port cannot be less than {MinPort} or greater than {MaxPort}");

            _portsToScan = Enumerable.Range(minPort, maxPort - minPort);

            SetupLists();
        }

        public PortScanner(IEnumerable<int> portsToScan)
        {
            if (portsToScan.Any(x => x < MinPort) || portsToScan.Any(x => x > MaxPort))
                throw new ArgumentOutOfRangeException($"Port cannot be less than {MinPort} or greater than {MaxPort}");

            _portsToScan = new List<int>(portsToScan);

            SetupLists();
        }

        public static async Task RunPortScanAsync(int minPort, int maxPort)
        {
            Console.WriteLine($"Checking ports {minPort} - {maxPort} on localhost...\n");

            var portScanner = new PortScanner(minPort, maxPort);

            var progress = new Progress<PortScanResult>();

            progress.ProgressChanged += (sender, args) =>
            {
                Console.WriteLine($"Port {args.PortNum} is {(args.IsPortOpen ? "open" : "closed")}");
            };

            await portScanner.ScanAsync(progress);
            portScanner.LastPortScanSummary();
        }

        public static async Task RunPortScanAsync(IEnumerable<int> portsToScan)
        {
            Console.WriteLine($"Checking specified range of ports on {Host}...\n");

            var portScanner = new PortScanner(portsToScan);

            var progress = new Progress<PortScanResult>();

            progress.ProgressChanged += (sender, args) =>
            {
                Console.WriteLine($"Port {args.PortNum} is {(args.IsPortOpen ? "open" : "closed")}");
            };

            await portScanner.ScanAsync(progress);
            portScanner.LastPortScanSummary();
        }

        public void LastPortScanSummary()
        {
            string openPorts = !_openPorts.Any() ? _openPorts.Count.ToString() : string.Join(",", _openPorts);

            string closedPorts = !_closedPorts.Any() ? _openPorts.Count.ToString() : string.Join(",", _closedPorts);

            Console.WriteLine();
            Console.WriteLine("-----------------");
            Console.WriteLine("Port Scan Results");
            Console.WriteLine("-----------------");
            Console.WriteLine();
            Console.WriteLine($"Open Ports......: {openPorts}");
            Console.WriteLine($"Closed Ports....: {closedPorts}");
            Console.WriteLine();
        }

        public async Task<bool> IsPortOpenAsync(int port)
        {
            Socket socket = null;

            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                await Task.Run(() => socket.Connect(Host, port));

                return true;
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.ConnectionRefused)
                    return false;

                Debug.WriteLine(ex.ToString());
                Console.WriteLine(ex);
            }
            finally
            {
                if (socket?.Connected ?? false)
                    socket?.Disconnect(false);

                socket?.Close();
            }

            return false;
        }

        private async Task CheckPortAsync(int port, IProgress<PortScanResult> progress)
        {
            if (await IsPortOpenAsync(port))
            {
                _openPorts.Add(port);
                progress?.Report(new PortScanResult { PortNum = port, IsPortOpen = true });
            }
            else
            {
                _closedPorts.Add(port);
                progress?.Report(new PortScanResult() { PortNum = port, IsPortOpen = false });
            }
        }

        private async Task ScanAsync(IProgress<PortScanResult> progress)
        {
            foreach (var port in _portsToScan)
                await CheckPortAsync(port, progress);
        }

        private void SetupLists()
        {
            _openPorts = new List<int>();
            _closedPorts = new List<int>();
        }
    }
}
