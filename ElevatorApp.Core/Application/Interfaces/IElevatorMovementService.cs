using ElevatorApp.Core.Domain;
using ElevatorApp.Core.Shared.Enums;

namespace ElevatorApp.Application.Interfaces
{
    public interface IElevatorMovementService
    {
        Task MoveElevatorAsync(Elevator elevator, int targetFloor, Direction requestedDirection);
    }
}
