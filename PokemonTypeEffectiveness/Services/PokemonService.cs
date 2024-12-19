using System.Net.Http.Json;
using PokemonTypeEffectiveness.Models;

namespace PokemonTypeEffectiveness.Services
{
    public class PokemonService
    {
        private readonly HttpClient _httpClient;

        public PokemonService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<PokemonTypeInfo?> GetPokemonTypeInfoAsync(string pokemonName)
        {
            try
            {
                var pokemonResponse = await _httpClient.GetFromJsonAsync<PokemonResponse>($"pokemon/{pokemonName.ToLower()}");
                if (pokemonResponse?.Types == null) return null;

                var typeUrl = pokemonResponse.Types[0].Type.Url;
                var typeResponse = await _httpClient.GetFromJsonAsync<TypeResponse>(typeUrl);

                if (typeResponse?.Damage_Relations == null) return null;

                var strengths = new List<string>();
                var weaknesses = new List<string>();

                strengths.AddRange(typeResponse.Damage_Relations.Double_Damage_To.Select(t => t.Name));
                strengths.AddRange(typeResponse.Damage_Relations.Half_Damage_From.Select(t => t.Name));
                strengths.AddRange(typeResponse.Damage_Relations.No_Damage_From.Select(t => t.Name));

                weaknesses.AddRange(typeResponse.Damage_Relations.Double_Damage_From.Select(t => t.Name));
                weaknesses.AddRange(typeResponse.Damage_Relations.Half_Damage_To.Select(t => t.Name));
                weaknesses.AddRange(typeResponse.Damage_Relations.No_Damage_To.Select(t => t.Name));

                return new PokemonTypeInfo
                {
                    Strengths = strengths.Distinct().ToList(),
                    Weaknesses = weaknesses.Distinct().ToList()
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
