using Fleck;

using Moq;
using NUnit.Framework;

using VoiceModFleckExercise.Factories;
using VoiceModFleckExercise.Services;
using VoiceModFleckExercise.Wrappers;

namespace VoiceModFleckExercise.Test.Services
{
	public class ServerServiceTests
	{
		private IServerService serverService;

		private Mock<IServerFactory> serverFactoryMock;
		private Mock<IConsoleWrapper> consoleWrapperMock;

		[SetUp]
		public void Setup()
		{
			serverFactoryMock = new Mock<IServerFactory>();

			consoleWrapperMock = new Mock<IConsoleWrapper>();

			consoleWrapperMock.Setup(m => m.ReadInput()).Returns("!exit");

			serverFactoryMock
				.Setup(m => m.CreateWebSocketServer(It.IsAny<int>()))
				.Returns(new WebSocketServer("ws://127.0.0.1:8181"));

			serverService = new ServerService(serverFactoryMock.Object, consoleWrapperMock.Object);
		}

		[Test]
		public void GivenValidParams_WhenCreateServer_AllOk()
		{
			serverService.CreateServer(8181);
			serverFactoryMock.Verify(mock => mock.CreateWebSocketServer(It.IsAny<int>()), Times.Once());
		}
	}
}
