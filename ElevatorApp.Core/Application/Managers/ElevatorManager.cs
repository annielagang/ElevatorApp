using ElevatorApp.Core.Application.Interfaces;

namespace ElevatorApp.Core.Application.Managers
{
    public class ElevatorManager
    {
        private readonly IElevatorDispatcherService _dispatcher;
        private readonly IRequestGeneratorService _requestGenerator;
        private bool _isRunning = true;
        private readonly int _delayBetweenRequests;

        public ElevatorManager(IElevatorDispatcherService dispatcher, IRequestGeneratorService requestGenerator, int delayBetweenRequests = 2000)
        {
            _dispatcher = dispatcher;
            _requestGenerator = requestGenerator;
            _delayBetweenRequests = delayBetweenRequests;
        }

        public async Task RunSimulationAsync()
        {
            while (_isRunning)
            {
                await Task.Delay(_delayBetweenRequests);
                await ProcessRequests();
            }
        }

        private async Task ProcessRequests()
        {
            var requests = _requestGenerator.GenerateRequests();
            foreach (var request in requests)
            {
                Console.WriteLine($"\n--- Processing request: Elevator {request.Elevator.Id} to floor {request.TargetFloor} ({request.Direction}). ---");
                await _dispatcher.DispatchRequestAsync(request);
            }
        }

        public void StopSimulation()
        {
            _isRunning = false;
        }
    }
}
