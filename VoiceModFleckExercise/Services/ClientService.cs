﻿using Fleck;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;

using VoiceModFleckExercise.Factories;
using VoiceModFleckExercise.Models;

namespace VoiceModFleckExercise.Services
{
	public class ClientService : IClientService
	{
		private readonly IClientFactory clientFactory;
		private readonly Regex usernamePattern = new Regex("^[a-zA-Z0-9]*$");

		private ClientModel Client { get; set; }

		public ClientService(IClientFactory clientFactory)
		{
			this.clientFactory = clientFactory;
		}

		public void CreateClient(int port)
		{
			string username;
			while (true)
			{
				Console.Write("Please, write your username: ");
				username = Console.ReadLine();

				if (!string.IsNullOrEmpty(username) && usernamePattern.IsMatch(username))
					break;

				Console.WriteLine("Invalid username!");
			}

			Client = clientFactory.CreateClientWebSocket(port, username);
			RunClient();

			FleckLog.Info("SubProtocol: " + Client.Socket.SubProtocol);
		}

		private void RunClient()
		{
			SendMessage($"[{DateTime.Now:HH:mm:ss}] - {Client.Username} has logged in!", false);
			Console.WriteLine(@"Type !exit to quit...");

			while (true)
			{
				Console.Write($"{Client.Username}: ");
				var userInput = Console.ReadLine();

				if (userInput == "!exit")
				{
					SendMessage($"[{DateTime.Now:HH:mm:ss}] - {Client.Username} has logged out!", false);
					if (Client.Socket.State == WebSocketState.Open)
					{
						var task = Client.Socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", Client.TokenSource.Token);
						task.Wait(Client.TokenSource.Token);
						task.Dispose();
					}
					Client.TokenSource.Dispose();
					Console.WriteLine("WebSocket disconnected.");
					return;
				}

				if (string.IsNullOrEmpty(userInput))
					continue;

				SendMessage(userInput);
			}
		}

		private void SendMessage(string userInput, bool useTemplate = true)
		{
			var message = useTemplate ? $"[{DateTime.Now:HH:mm:ss}] - {Client.Username}: {userInput}" : userInput;

			var task = Client.Socket.SendAsync(
				new ArraySegment<byte>(Encoding.UTF8.GetBytes(message)),
				WebSocketMessageType.Text,
				true,
				Client.TokenSource.Token
			);

			task.Wait(Client.TokenSource.Token); 
			task.Dispose();
			Console.WriteLine($"{message}");
		}
	}
}
