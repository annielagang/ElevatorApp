using ElevatorApp.Core.Domain;
using ElevatorApp.Core.Shared.Enums;

namespace ElevatorApp.Core.Shared.Requests
{
    public class ElevatorRequest
    {
        public Elevator Elevator { get; }
        public int TargetFloor { get; }
        public Direction Direction { get; }

        public ElevatorRequest(Elevator elevator, int targetFloor, Direction direction)
        {
            Elevator = elevator ?? throw new ArgumentNullException(nameof(elevator));
            TargetFloor = targetFloor;
            Direction = direction;
        }
    }
}
