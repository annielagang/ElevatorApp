using ElevatorApp.Application.Interfaces;
using ElevatorApp.Core.Application.Services;
using ElevatorApp.Core.Domain;
using ElevatorApp.Domain;
using Moq;

namespace ElevatorApp.Tests
{
    public class RequestGeneratorServiceTests
    {
        private readonly Building _building;
        private readonly RequestGeneratorService _requestGenerator;

        public RequestGeneratorServiceTests()
        {
            _building = new Building(10, 4, Mock.Of<IElevatorMovementService>());
            _requestGenerator = new RequestGeneratorService(_building);
        }

        [Fact]
        public void GenerateRequests_ShouldReturnRequests_WhenElevatorsAreAvailable()
        {
            _building.Elevators[0].SetCurrentFloor(3);
            _building.Elevators[1].SetCurrentFloor(5);
            _building.Elevators[2].SetCurrentFloor(7);
            _building.Elevators[3].SetCurrentFloor(9);

            var requests = _requestGenerator.GenerateRequests();

            Assert.NotEmpty(requests);
            Assert.All(requests, request =>
            {
                Assert.NotNull(request.Elevator);
                Assert.NotEqual(request.Elevator.CurrentFloor, request.TargetFloor);
            });
        }

        [Fact]
        public void GenerateRequests_ShouldReturnEmptyList_WhenAllElevatorsAreBusy()
        {
            foreach (var elevator in _building.Elevators)
            {
                typeof(Elevator).GetProperty(nameof(Elevator.IsCurrentlyMoving))?.SetValue(elevator, true);
            }

            var requests = _requestGenerator.GenerateRequests();

            Assert.Empty(requests);
        }

        [Fact]
        public void GenerateRequests_ShouldGenerateValidFloorRequests()
        {
            var requests = _requestGenerator.GenerateRequests();

            Assert.All(requests, request =>
            {
                Assert.InRange(request.TargetFloor, 1, _building.NumberOfFloors);
            });
        }
    }
}



