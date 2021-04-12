using System;
using System.Net.WebSockets;
using System.Threading;

using Fleck;

using Microsoft.Extensions.Configuration;

using VoiceModFleckExercise.Models;

namespace VoiceModFleckExercise.Factories
{
	public class ClientFactory : AbstractFactory, IClientFactory
	{
		public ClientFactory(IConfiguration configuration)
		:base(configuration)
		{
		}

		public ClientModel CreateClientWebSocket(int port, string username)
		{
			var clientWebSocket = new ClientWebSocket();
			var tokenSource = new CancellationTokenSource();

			try
			{
				var task = clientWebSocket.ConnectAsync(new Uri($"{BaseUrl}{port}"), tokenSource.Token);

				task.Wait(tokenSource.Token);
				task.Dispose();

				FleckLog.Info($"WebSocket to {BaseUrl}{port} connected!");

				return new ClientModel(username, clientWebSocket, tokenSource);
			}
			catch (Exception e)
			{
				var exMessage = $"Error trying to connect: {e}";
				FleckLog.Error(exMessage);
				Console.WriteLine(exMessage);
				throw;
			}
		}
	}
}
