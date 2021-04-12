using System;
using System.IO;

using Fleck;

using log4net;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using VoiceModFleckExercise.Factories;
using VoiceModFleckExercise.Services;

using LogLevel = Fleck.LogLevel;

namespace VoiceModFleckExercise
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			// Build logging
			var logger = LogManager.GetLogger(typeof(FleckLog));

			FleckLog.LogAction = (level, message, ex) => {
				switch (level)
				{
					case LogLevel.Debug:
						logger.Debug(message, ex);
						break;
					case LogLevel.Error:
						logger.Error(message, ex);
						break;
					case LogLevel.Warn:
						logger.Warn(message, ex);
						break;
					default:
						logger.Info(message, ex);
						break;
				}
			};

			// Build configuration
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
				.AddJsonFile("appsettings.json", false)
				.Build();

			// Add logging and configuration
			services.AddSingleton(logger);
			services.AddSingleton<IConfiguration>(configuration);

			// Add dependencies
			services.AddSingleton<IServerFactory, ServerFactory>();
			services.AddSingleton<IClientFactory, ClientFactory>();

			services.AddTransient<IServerService, ServerService>();
			services.AddTransient<IClientService, ClientService>();
			services.AddTransient<MainService>();
		}
	}
}
