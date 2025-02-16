using ElevatorApp.Core.Application.Interfaces;
using ElevatorApp.Core.Domain;
using ElevatorApp.Core.Shared.Enums;
using ElevatorApp.Core.Shared.Requests;
using ElevatorApp.Domain;

namespace ElevatorApp.Core.Application.Services
{
    public class ElevatorDispatcherService : IElevatorDispatcherService
    {
        private readonly Building _building;

        public ElevatorDispatcherService(Building building)
        {
            _building = building ?? throw new ArgumentNullException(nameof(building));
        }

        public async Task DispatchRequestAsync(ElevatorRequest request)
        {
            var elevator = TryFindElevator(request.Elevator.Id, request.TargetFloor, request.Direction);

            if (elevator == null)
            {
                Console.WriteLine($"No available elevators for request at floor {request.TargetFloor}.");
                return;
            }

            await elevator.MoveToFloorAsync(request.TargetFloor, request.Direction, () =>
            {
                ShowAllElevatorStatuses();
            });
        }

        private Elevator? TryFindElevator(int elevatorId, int requestedFloor, Direction direction)
        {
            Elevator? elevator = _building.Elevators.FirstOrDefault(e => e.Id == elevatorId);
            if (elevator == null)
            {
                Console.WriteLine($"Elevator {elevatorId} does not exist.");
            }
            else
            {
                if (elevator.CurrentFloor == requestedFloor)
                {
                    Console.WriteLine($"Elevator {elevatorId} is already in floor {elevator.CurrentFloor}, going idle.");
                }
            }

            return elevator;
        }

        public void ShowAllElevatorStatuses()
        {
            Console.WriteLine("\n--- Elevator Status Summary ---");
            foreach (var elevator in _building.Elevators)
            {
                string status = elevator.HasPendingRequest ? elevator.Direction.ToString() : "Idle";
                Console.WriteLine($"Elevator {elevator.Id}: Floor {elevator.CurrentFloor}, Status: {status}");
            }
            Console.WriteLine("--------------------------------\n");
        }
    }
}
