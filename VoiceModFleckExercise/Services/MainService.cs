using System.Linq;
using System.Net.NetworkInformation;
using VoiceModFleckExercise.Wrappers;

namespace VoiceModFleckExercise.Services
{
	public class MainService
	{
		private readonly IServerService serverService;
		private readonly IClientService clientService;
		private readonly IConsoleWrapper consoleWrapper;

		public MainService(IServerService serverService, IClientService clientService, IConsoleWrapper consoleWrapper)
		{
			this.serverService = serverService;
			this.clientService = clientService;
			this.consoleWrapper = consoleWrapper;
		}

		// I personally would have created a project for the server and another for the client!
		public void Run(int port)
		{
			if (PortIsListening(port))
			{
				consoleWrapper.SendOutput($"Server with port {port} found!");
				clientService.CreateClient(port);
			}
			else
			{
				consoleWrapper.SendOutput($"The port {port} is not active, creating server on this port.");
				serverService.CreateServer(port);
			}
		}

		private bool PortIsListening(int port)
		{
			consoleWrapper.SendOutput($"Checking if port {port} is listening...");
			var endPoints = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners().ToList();
			return endPoints.Any(endPoint => endPoint.Port == port);
		}
	}
}
