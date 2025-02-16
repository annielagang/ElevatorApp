using ElevatorApp.Core.Application.Interfaces;
using ElevatorApp.Core.Shared.Enums;
using ElevatorApp.Core.Shared.Requests;
using ElevatorApp.Domain;

namespace ElevatorApp.Core.Application.Services
{
    public class RequestGeneratorService : IRequestGeneratorService
    {
        private readonly Building _building;
        private readonly Random _random;

        public RequestGeneratorService(Building building)
        {
            _building = building ?? throw new ArgumentNullException(nameof(building));
            _random = new Random();
        }

        public List<ElevatorRequest> GenerateRequests()
        {
            var requests = new List<ElevatorRequest>();

            foreach (var elevator in _building.Elevators)
            {
                if (!elevator.IsCurrentlyMoving)
                {
                    int targetFloor = _random.Next(1, _building.NumberOfFloors + 1);

                    if (targetFloor != elevator.CurrentFloor)
                    {
                        Direction direction = targetFloor > elevator.CurrentFloor ? Direction.Up : Direction.Down;
                        Console.WriteLine($"Generated request: Elevator {elevator.Id} to floor {targetFloor} ({direction}).");

                        requests.Add(new ElevatorRequest(elevator, targetFloor, direction));
                        elevator.HasPendingRequest = true;
                    }
                }
            }

            return requests;
        }
    }
}
