using Moq;

using NUnit.Framework;
using System;
using System.Net.WebSockets;
using System.Threading;

using VoiceModFleckExercise.Factories;
using VoiceModFleckExercise.Models;
using VoiceModFleckExercise.Services;
using VoiceModFleckExercise.Wrappers;

namespace VoiceModFleckExercise.Test.Services
{
	public class ClientServiceTests
	{
		private IClientService clientService;

		private Mock<IClientFactory> clientFactoryMock;
		private Mock<IConsoleWrapper> consoleWrapperMock;

		[SetUp]
		public void Setup()
		{
			clientFactoryMock = new Mock<IClientFactory>();
			consoleWrapperMock = new Mock<IConsoleWrapper>();

			consoleWrapperMock.Setup(m => m.ReadInput()).Returns("test");

			var clientModelTest = new ClientModel("test",
				new ClientWebSocket(),
				new CancellationTokenSource());

			clientFactoryMock
				.Setup(m => m.CreateClientWebSocket(It.IsAny<int>(), It.IsAny<string>()))
				.Returns(clientModelTest);


			clientService = new ClientService(clientFactoryMock.Object, consoleWrapperMock.Object);
		}

		[Test]
		public void GivenValidParameters_WhenExecuting_FailDueToServerDoesntExist()
		{
			Assert.Throws<InvalidOperationException>(() => this.clientService.CreateClient(1));
		}
	}
}
