using ElevatorApp.Application.Interfaces;
using ElevatorApp.Core.Domain;
using ElevatorApp.Core.Shared.Enums;
using Moq;

namespace ElevatorApp.Tests
{
    public class ElevatorTests
    {
        private readonly Mock<IElevatorMovementService> _mockMovementService;
        private readonly Elevator _elevator;

        public ElevatorTests()
        {
            _mockMovementService = new Mock<IElevatorMovementService>();
            _elevator = new Elevator(1, 10, _mockMovementService.Object);
        }

        [Fact]
        public async Task MoveToFloorAsync_ValidFloor_ShouldInvokeMovementService()
        {
            int targetFloor = 5;
            Direction direction = Direction.Up;
            _mockMovementService.Setup(m => m.MoveElevatorAsync(_elevator, targetFloor, Direction.Up))
                .Returns(Task.CompletedTask);

            await _elevator.MoveToFloorAsync(targetFloor, direction, null);

            _mockMovementService.Verify(m => m.MoveElevatorAsync(_elevator, targetFloor, Direction.Up), Times.Once);
        }

        [Fact]
        public async Task MoveToFloorAsync_SameFloor_ShouldNotInvokeMovementService()
        {
            int targetFloor = _elevator.CurrentFloor;
            Direction direction = Direction.Idle;

            await _elevator.MoveToFloorAsync(targetFloor, direction, null);

            _mockMovementService.Verify(m => m.MoveElevatorAsync(It.IsAny<Elevator>(), It.IsAny<int>(), Direction.Idle), Times.Never);
        }

        [Fact]
        public async Task MoveToFloorAsync_ShouldUpdateCurrentFloor()
        {
            int targetFloor = 7;
            Direction direction = Direction.Up;
            _mockMovementService.Setup(m => m.MoveElevatorAsync(It.IsAny<Elevator>(), It.IsAny<int>(), Direction.Up))
                .Callback<Elevator, int, Direction>((elevator, floor, direction) => elevator.SetCurrentFloor(floor))
                .Returns(Task.CompletedTask);

            await _elevator.MoveToFloorAsync(targetFloor, direction, null);

            Assert.Equal(targetFloor, _elevator.CurrentFloor);
        }
    }
}
