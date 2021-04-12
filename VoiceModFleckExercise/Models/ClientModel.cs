using System.Net.WebSockets;
using System.Threading;

namespace VoiceModFleckExercise.Models
{
	public class ClientModel
	{
		public ClientModel(string username, ClientWebSocket socket, CancellationTokenSource tokenSource)
		{
			Username = username;
			Socket = socket;
			TokenSource = tokenSource;
		}

		public string Username { get; }

		public ClientWebSocket Socket { get; }

		public CancellationTokenSource TokenSource { get; }
	}
}
