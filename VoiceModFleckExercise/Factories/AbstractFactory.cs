using Microsoft.Extensions.Configuration;

namespace VoiceModFleckExercise.Factories
{
	public abstract class AbstractFactory
	{
		protected const string BaseUrl = "ws://127.0.0.1:";
		protected readonly IConfiguration Configuration;

		protected AbstractFactory(IConfiguration configuration)
		{
			Configuration = configuration;
		}
	}
}
