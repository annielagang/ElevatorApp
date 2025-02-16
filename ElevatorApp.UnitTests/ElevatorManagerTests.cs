using ElevatorApp.Application.Interfaces;
using ElevatorApp.Core.Application.Interfaces;
using ElevatorApp.Core.Application.Managers;
using ElevatorApp.Core.Domain;
using ElevatorApp.Core.Shared.Enums;
using ElevatorApp.Core.Shared.Requests;
using Moq;

namespace ElevatorApp.Tests
{
    public class ElevatorManagerTests
    {
        private readonly Mock<IElevatorDispatcherService> _mockDispatcher;
        private readonly Mock<IRequestGeneratorService> _mockRequestGenerator;
        private readonly Mock<IElevatorMovementService> _mockMovementService;
        private readonly ElevatorManager _manager;
        public ElevatorManagerTests()
        {
            _mockDispatcher = new Mock<IElevatorDispatcherService>();
            _mockRequestGenerator = new Mock<IRequestGeneratorService>();

            _manager = new ElevatorManager(_mockDispatcher.Object, _mockRequestGenerator.Object, 1000);
            _mockMovementService = new Mock<IElevatorMovementService>();
        }

        [Fact]
        public async Task RunSimulationAsync_ShouldProcessRequests()
        {
            var mockRequests = new List<ElevatorRequest>
            {
                new ElevatorRequest(new Elevator(1, 10, _mockMovementService.Object), 5, Direction.Up),
                new ElevatorRequest(new Elevator(1, 10, _mockMovementService.Object), 8, Direction.Down)
            };

            _mockRequestGenerator.Setup(r => r.GenerateRequests()).Returns(mockRequests);

            _mockDispatcher.Setup(d => d.DispatchRequestAsync(It.IsAny<ElevatorRequest>()))
                .Returns(Task.CompletedTask);

            var simulationTask = _manager.RunSimulationAsync();

            await Task.Delay(2500);

            _manager.StopSimulation();
            await simulationTask;

            _mockDispatcher.Verify(d => d.DispatchRequestAsync(It.IsAny<ElevatorRequest>()), Times.AtLeastOnce);
        }
    }
}
