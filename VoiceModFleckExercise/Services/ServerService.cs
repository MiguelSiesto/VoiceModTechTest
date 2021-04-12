using System;
using System.Collections.Generic;
using System.Linq;

using Fleck;

using VoiceModFleckExercise.Factories;

namespace VoiceModFleckExercise.Services
{
	public class ServerService : IServerService
	{
		private readonly IServerFactory serverFactory;
		private readonly IList<IWebSocketConnection> sockets;

		private WebSocketServer Server { get; set; }

		public ServerService(IServerFactory serverFactory)
		{
			this.serverFactory = serverFactory;
			sockets = new List<IWebSocketConnection>();
		}
		
		public void CreateServer(int port)
		{
			Console.WriteLine("Launching server...");
			Server = serverFactory.CreateWebSocketServer(port);

			Server.Start(socket =>
			{
				socket.OnOpen = () =>
				{
					FleckLog.Info("Connection open.");
					sockets.Add(socket);
				};
				socket.OnClose = () =>
				{
					FleckLog.Info("Connection closed.");
					sockets.Remove(socket);
				};
				socket.OnMessage = message =>
				{
					FleckLog.Info("Client message: " + message);
					Console.WriteLine($"{message}");
					sockets.ToList().ForEach(s => s.Send(message));
				};
			});

			while (true)
			{
				var input = Console.ReadLine();

				if (input == "!exit")
				{
					Console.WriteLine("Closing server...");
					return;
				}

				sockets.ToList().ForEach(s => s.Send(input));
			}
		}
	}
}
