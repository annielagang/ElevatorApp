using ElevatorApp.Application.Interfaces;
using ElevatorApp.Core.Domain;

namespace ElevatorApp.Domain
{
    public class Building
    {
        public int NumberOfFloors { get; }
        public List<Elevator> Elevators { get; }

        public Building(int numberOfFloors, int numberOfElevators, IElevatorMovementService movementService)
        {
            if (numberOfFloors < 2)
                throw new ArgumentException("A building must have at least 2 floors.", nameof(numberOfFloors));

            if (numberOfElevators < 1)
                throw new ArgumentException("A building must have at least 1 elevator.", nameof(numberOfElevators));

            NumberOfFloors = numberOfFloors;

            Elevators = Enumerable.Range(1, numberOfElevators)
                .Select(id => new Elevator(id, numberOfFloors, movementService))
                .ToList();

            Console.WriteLine($"Building initialized with {numberOfFloors} floors and {numberOfElevators} elevators.");
        }
    }
}
