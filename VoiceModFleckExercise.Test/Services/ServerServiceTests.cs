using Fleck;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using VoiceModFleckExercise.Factories;
using VoiceModFleckExercise.Services;

namespace VoiceModFleckExercise.Test.Services
{
	public class ServerServiceTests
	{
		private IServerService serverService;

		private Mock<IServerFactory> serverFactoryMock;

		[SetUp]
		public void Setup()
		{
			serverFactoryMock = new Mock<IServerFactory>();

			serverFactoryMock
				.Setup(m => m.CreateWebSocketServer(It.IsAny<int>()))
				.Returns(new WebSocketServer("ws://127.0.0.1:8181"));

			serverService = new ServerService(serverFactoryMock.Object);
		}

		// Same as ClientServiceTests, the input should be refactored to a wrapper to do proper testing.

		//[Test]
		//public void GivenValidParams_WhenCreateServer_AllOk()
		//{
		//	serverService.CreateServer(8181);
		//	serverFactoryMock.Verify(mock => mock.CreateWebSocketServer(It.IsAny<int>()), Times.Once());
		//}
	}
}
