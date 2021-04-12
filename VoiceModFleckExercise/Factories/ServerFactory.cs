using Fleck;

using Microsoft.Extensions.Configuration;

namespace VoiceModFleckExercise.Factories
{
	public class ServerFactory : AbstractFactory, IServerFactory
	{
		public ServerFactory(IConfiguration configuration)
		: base(configuration)
		{
		}

		public WebSocketServer CreateWebSocketServer(int port)
		{
			var restartOnListenError = !bool.TryParse(Configuration["Settings:RestartOnListenError"], out var value)
									   || value;

			return new WebSocketServer($"{BaseUrl}{port}") { RestartAfterListenError = restartOnListenError };
		}
	}
}
