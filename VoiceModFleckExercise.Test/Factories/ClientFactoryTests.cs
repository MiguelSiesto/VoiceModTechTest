using System;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using VoiceModFleckExercise.Factories;

namespace VoiceModFleckExercise.Test.Factories
{
	public class ClientFactoryTests
	{
		private IClientFactory clientFactory;
		private Mock<IConfiguration> configurationMock;

		[SetUp]
		public void Setup()
		{
			configurationMock = new Mock<IConfiguration>();

			clientFactory = new ClientFactory(configurationMock.Object);
		}

		[Test]
		public void GivenParameters_WhenCreatingClient_FailsDueToNoServerExists()
		{
			Assert.Throws<AggregateException>(() => this.clientFactory.CreateClientWebSocket(1, "test"));
		}
	}
}
