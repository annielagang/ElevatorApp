using ElevatorApp.Application.Interfaces;
using ElevatorApp.Core.Shared.Enums;

namespace ElevatorApp.Core.Domain
{
    public class Elevator
    {
        public int Id { get; }
        public int CurrentFloor { get; private set; } = 1;
        public int TargetFloor { get; private set; } = 1;
        public bool IsCurrentlyMoving { get; private set; }
        public bool HasPendingRequest { get; set; } = false;
        public Direction Direction { get; set; } = Direction.Idle;
        private readonly int _maxFloor;
        private readonly IElevatorMovementService _movementService;

        public Elevator(int id, int maxFloor, IElevatorMovementService movementService)
        {
            Id = id;
            _maxFloor = maxFloor;
            _movementService = movementService;
        }

        public async Task MoveToFloorAsync(int targetFloor, Direction requestedDirection, Action onArrival)
        {
            TargetFloor = targetFloor;

            if (targetFloor < 1 || targetFloor > _maxFloor)
            {
                Console.WriteLine($"Invalid floor request for Elevator {Id}. Floor must be between 1 and {_maxFloor}.");
                return;
            }

            if (targetFloor == CurrentFloor)
            {
                Console.WriteLine($"Elevator {Id} is already on floor {CurrentFloor}.");
                return;
            }

            IsCurrentlyMoving = true;
            Direction = requestedDirection;
            await _movementService.MoveElevatorAsync(this, targetFloor, requestedDirection);
            onArrival?.Invoke();
            IsCurrentlyMoving = false;
            HasPendingRequest = false;
        }

        public void SetCurrentFloor(int floor)
        {
            CurrentFloor = floor;
        }
    }
}
