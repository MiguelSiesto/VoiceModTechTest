using System;
using System.Linq;
using System.Net.NetworkInformation;

namespace VoiceModFleckExercise.Services
{
	public class MainService
	{
		private readonly IServerService serverService;

		private readonly IClientService clientService;

		public MainService(IServerService serverService, IClientService clientService)
		{
			this.serverService = serverService;
			this.clientService = clientService;
		}

		// I personally would have created a project for the server and another for the client!
		public void Run(int port)
		{
			if (PortIsListening(port))
			{
				Console.WriteLine($"Server with port {port} found!");
				clientService.CreateClient(port);
			}
			else
			{
				Console.WriteLine($"The port {port} is not active, creating server on this port.");
				serverService.CreateServer(port);
			}
		}

		private bool PortIsListening(int port)
		{
			Console.WriteLine($"Checking if port {port} is listening...");
			var endPoints = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners().ToList();
			return endPoints.Any(endPoint => endPoint.Port == port);
		}
	}
}
