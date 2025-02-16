using ElevatorApp.Application.Interfaces;
using ElevatorApp.Core.Application.Services;
using ElevatorApp.Core.Shared.Enums;
using ElevatorApp.Core.Shared.Requests;
using ElevatorApp.Domain;
using Moq;

namespace ElevatorApp.Tests
{
    public class ElevatorDispatcherServiceTests
    {
        private readonly Building _building;
        private readonly ElevatorDispatcherService _dispatcher;

        public ElevatorDispatcherServiceTests()
        {
            _building = new Building(10, 4, Mock.Of<IElevatorMovementService>());
            _dispatcher = new ElevatorDispatcherService(_building);
        }

        [Fact]
        public async Task DispatchRequestAsync_ShouldMoveElevatorToTargetFloor()
        {
            var mockMovementService = new Mock<IElevatorMovementService>();
            var testBuilding = new Building(10, 4, mockMovementService.Object);
            var dispatcher = new ElevatorDispatcherService(testBuilding);
            var request = new ElevatorRequest(testBuilding.Elevators[0], 8, Direction.Up);

            await dispatcher.DispatchRequestAsync(request);

            mockMovementService.Verify(m => m.MoveElevatorAsync(testBuilding.Elevators[0], request.TargetFloor, Direction.Up), Times.Once);
        }
    }
}
