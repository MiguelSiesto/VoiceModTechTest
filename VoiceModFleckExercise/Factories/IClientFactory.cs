using VoiceModFleckExercise.Models;

namespace VoiceModFleckExercise.Factories
{
	public interface IClientFactory
	{
		public ClientModel CreateClientWebSocket(int port, string username);
	}
}
