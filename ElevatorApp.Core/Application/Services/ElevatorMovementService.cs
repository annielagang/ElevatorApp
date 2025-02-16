using ElevatorApp.Application.Interfaces;
using ElevatorApp.Core.Domain;
using ElevatorApp.Core.Shared.Enums;

namespace ElevatorApp.Core.Application
{
    public class ElevatorMovementService : IElevatorMovementService
    {
        private const int CarMoveTimeInMs = 10000;
        private const int PassengerUnloadTimeInMs = 10000;

        public async Task MoveElevatorAsync(Elevator elevator, int targetFloor, Direction requestedDirection)
        {
            elevator.HasPendingRequest = true;
            elevator.Direction = requestedDirection;

            Console.WriteLine($"Elevator {elevator.Id} moving from floor {elevator.CurrentFloor} to floor {targetFloor}.");

            while (elevator.CurrentFloor != targetFloor)
            {
                await Task.Delay(CarMoveTimeInMs);

                elevator.SetCurrentFloor(elevator.CurrentFloor + (requestedDirection == Direction.Up ? 1 : -1));
                Console.WriteLine($"Elevator {elevator.Id} reached floor {elevator.CurrentFloor}.");
            }

            Console.WriteLine($"Elevator {elevator.Id} has arrived at floor {targetFloor}. Unloading passengers...");
            Console.WriteLine("--------------------------------\n");
            await Task.Delay(PassengerUnloadTimeInMs);

            elevator.Direction = Direction.Idle;
            elevator.HasPendingRequest = false;
        }
    }
}
