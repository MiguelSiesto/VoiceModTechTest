using System.Collections.Generic;
using System.Linq;

using Fleck;

using VoiceModFleckExercise.Factories;
using VoiceModFleckExercise.Wrappers;

namespace VoiceModFleckExercise.Services
{
	public class ServerService : IServerService
	{
		private readonly IServerFactory serverFactory;
		private readonly IConsoleWrapper consoleWrapper;

		private readonly IList<IWebSocketConnection> sockets;

		private WebSocketServer Server { get; set; }

		public ServerService(IServerFactory serverFactory, IConsoleWrapper consoleWrapper)
		{
			this.serverFactory = serverFactory;
			this.consoleWrapper = consoleWrapper;
			sockets = new List<IWebSocketConnection>();
		}
		
		public void CreateServer(int port)
		{
			consoleWrapper.SendOutput("Launching server...");
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
					consoleWrapper.SendOutput($"{message}");
					sockets.ToList().ForEach(s => s.Send(message));
				};
			});

			while (true)
			{
				var input = consoleWrapper.ReadInput();

				if (input == "!exit")
				{
					consoleWrapper.SendOutput("Closing server...");
					return;
				}

				sockets.ToList().ForEach(s => s.Send(input));
			}
		}
	}
}
