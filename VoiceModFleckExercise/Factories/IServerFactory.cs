using Fleck;

namespace VoiceModFleckExercise.Factories
{
	public interface IServerFactory
	{
		public WebSocketServer CreateWebSocketServer(int port);
	}
}
