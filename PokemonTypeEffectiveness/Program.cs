using Microsoft.Extensions.DependencyInjection;
using PokemonTypeEffectiveness.Services;

namespace PokemonTypeEffectiveness
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = ConfigureServices();

            var pokemonService = serviceProvider.GetRequiredService<PokemonService>();

            Console.WriteLine("Pokémon Type Effectiveness Checker");

            while (true)
            {
                Console.WriteLine("----------------------------------");
                Console.Write("Enter a Pokémon name: ");
                var pokemonName = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(pokemonName))
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    continue;
                }

                try
                {
                    var typeInfo = await pokemonService.GetPokemonTypeInfoAsync(pokemonName);

                    if (typeInfo != null)
                    {
                        Console.WriteLine($"\nPokémon: {pokemonName}");

                        Console.WriteLine("\nStrengths:");
                        Console.WriteLine(typeInfo.Strengths.Count > 0
                            ? string.Join("\n- ", typeInfo.Strengths.Prepend("- "))
                            : "- None");

                        Console.WriteLine("\nWeaknesses:");
                        Console.WriteLine(typeInfo.Weaknesses.Count > 0
                            ? string.Join("\n- ", typeInfo.Weaknesses.Prepend("- "))
                            : "- None");
                    }
                    else
                    {
                        Console.WriteLine("Could not retrieve Pokémon data. Please ensure the name is correct.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        private static ServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddHttpClient<PokemonService>(client =>
            {
                client.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
            });

            return serviceCollection.BuildServiceProvider();
        }
    }
}
