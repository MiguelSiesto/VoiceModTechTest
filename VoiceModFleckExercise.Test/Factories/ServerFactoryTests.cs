using System;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using VoiceModFleckExercise.Factories;

namespace VoiceModFleckExercise.Test.Factories
{
	public class ServerFactoryTests
	{

		private Mock<IConfiguration> configurationMock;

		private ServerFactory serverFactory;

		[SetUp]
		public void Setup()
		{
			configurationMock = new Mock<IConfiguration>();

			serverFactory = new ServerFactory(configurationMock.Object);
		}

		[Test]
		public void GivenValidParameters_WhenGettingServer_AllOk()
		{
			var result = serverFactory.CreateWebSocketServer(1);

			Assert.IsNotNull(result);
			Assert.IsTrue(result.Port == 1);
		}

		[Test]
		public void GivenInvalidParameters_WhenGettingServer_Fail()
		{
			Assert.Throws<UriFormatException>(() => serverFactory.CreateWebSocketServer(999999));
		}
	}
}
