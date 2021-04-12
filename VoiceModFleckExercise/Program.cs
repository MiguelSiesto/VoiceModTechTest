using System;
using System.Linq;
using System.Text;

using Fleck;

using Microsoft.Extensions.DependencyInjection;

using VoiceModFleckExercise.Services;

namespace VoiceModFleckExercise
{
	public class Program
	{
		private static int Main(string[] args)
		{
			try
			{
				Console.OutputEncoding = Encoding.UTF8;

				Console.WriteLine("Initializing...");
				if (!args.Any() || !int.TryParse(args[0], out var port) || port > 65353 || port < 0)
					throw new Exception($"Critical error starting server, arguments are not correct: {args}");

				// Create service collection
				var serviceCollection = new ServiceCollection();

				Console.WriteLine("Loading configuration...");
				var startup = new Startup();
				startup.ConfigureServices(serviceCollection);

				// Create service provider
				var serviceProvider = serviceCollection.BuildServiceProvider();
				serviceProvider.GetService<MainService>().Run(port);

				return 0;
			}
			catch (Exception e)
			{
				var errorMessage = $"Critical Error: {e}";
				FleckLog.Error(errorMessage);
				Console.WriteLine(errorMessage);
				return 1;
			}
		}
	}
}
