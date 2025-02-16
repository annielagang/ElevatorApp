using ElevatorApp.Core.Shared.Requests;

namespace ElevatorApp.Core.Application.Interfaces
{
    public interface IElevatorDispatcherService
    {
        Task DispatchRequestAsync(ElevatorRequest request);
    }
}
