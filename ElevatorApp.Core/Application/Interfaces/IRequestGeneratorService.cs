using ElevatorApp.Core.Shared.Requests;

namespace ElevatorApp.Core.Application.Interfaces
{
    public interface IRequestGeneratorService
    {
        List<ElevatorRequest> GenerateRequests();
    }
}
