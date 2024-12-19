                    Pokemon Type Effectiveness Application

This application provides information about a Pokémon's strengths and weaknesses based on Name Provided.

Prerequisites
.NET SDK: Install the latest version of the .NET SDK from here.
Visual Studio (2022 or later) or Visual Studio Code with the C# extension.

Steps to Run the Application

1. Clone or Download the Repository
   Clone the repository using Git:
   git clone <path-for-git-repo>
   Or download the repository as a ZIP file and extract it.

2. Open the Project in an IDE

   Visual Studio:
   Open Visual Studio.
   Click on File > Open > Project/Solution.
   Select the .sln file located in the project directory.
   Restore NuGet packages by building the solution (Ctrl+Shift+B).

   Visual Studio Code:
   Open VS Code.
   Open the folder containing the project (File > Open Folder).
   Install the C# extension if prompted.
   Restore NuGet packages by running the following command in the integrated terminal: dotnet restore

3. Build and Run the Application
   dotnet run --project PokemonTypeEffectiveness
   Alternatively, press F5 in Visual Studio or VS Code to start the application with debugging enabled.

4. Input a Pokémon Name
   The application will prompt you to input a Pokémon name.
   Example: pikachu.

5. View the Output
   The application will display the Pokémon's Strengths and Weaknesses.

Running Unit Tests with .NET CLI
You can run the unit tests directly from the command line using the .NET CLI:

Open the Integrated Terminal in your IDE (like Visual Studio or Visual Studio Code) or use the command prompt.

Navigate to the Test Project Directory: Make sure you're in the folder containing the unit test project (PokemonTypeEffectiveness.Tests). If you're not already in that directory, navigate to it:

cd PokemonTypeEffectiveness.Tests

Run the Tests: dotnet test
