# Elevator Simulation in .NET Core

## Overview
This is a .NET Core console application that simulates multiple elevators operating in a 10-floor building. The simulation tracks elevator location, movement, and direction while processing randomly generated requests asynchronously.

## Features
- **Building Setup:** 10-floor building with 4 elevators.
- **Elevator Movement:** Tracks location, movement, and direction of each elevator.
- **Asynchronous Processing:** Handles elevator requests asynchronously.
- **Randomized Requests:** Generates new requests after previous requests are processed.
- **Conflict Handling:** Prevents duplicate requests for occupied elevators.
- **Periodic Summaries:** Displays elevator statuses in a readable format.
- **Graceful Shutdown:** Typing `end` stops request generation and processes all pending requests before exiting.

## Technologies Used
- **C# .NET Core** - For building the simulation.
- **xUnit** - For unit testing.
- **Moq** - For mocking test data.

## Testing
Unit tests are implemented using **xUnit** with **Moq** for dummy data. The test coverage includes:
- Elevator movement.
- Stopping logic.
- Direction switching.
- Passenger loading/unloading.

## How to Run
1. Clone the repository:
   ```sh
   git clone <repository-url>
   cd elevator-simulation
   ```
2. Build the project:
   ```sh
   dotnet build
   ```
3. Run the simulation:
   ```sh
   dotnet run
   ```
4. To stop the simulation, type `end` in the console.

## Contributing
Feel free to submit issues or contribute improvements via pull requests.

## License
This project is licensed under the MIT License.
