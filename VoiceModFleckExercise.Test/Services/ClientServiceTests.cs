using Moq;

using NUnit.Framework;
using System;
using System.IO;
using System.Net.WebSockets;
using System.Threading;

using VoiceModFleckExercise.Factories;
using VoiceModFleckExercise.Models;
using VoiceModFleckExercise.Services;

namespace VoiceModFleckExercise.Test.Services
{
	public class ClientServiceTests
	{
		private IClientService clientService;

		private Mock<IClientFactory> clientFactoryMock;

		[SetUp]
		public void Setup()
		{
			clientFactoryMock = new Mock<IClientFactory>();

			var clientModelTest = new ClientModel("test",
				new ClientWebSocket(),
				new CancellationTokenSource());

			clientFactoryMock
				.Setup(m => m.CreateClientWebSocket(It.IsAny<int>(), It.IsAny<string>()))
				.Returns(clientModelTest);

			clientService = new ClientService(clientFactoryMock.Object);
		}

		// The code should be refactored using a Console Wrapper that implements an Interface..
		// This way, the wrapper could be mocked and thus, a bit more of this class tested.

		//[Test]
		//public void GivenValid_WhenExecuting_Fail()
		//{
		//	clientService.CreateClient(2);			
		//}
	}
}
