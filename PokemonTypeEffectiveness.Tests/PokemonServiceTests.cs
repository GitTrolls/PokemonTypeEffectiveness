using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using PokemonTypeEffectiveness.Services;
using PokemonTypeEffectiveness.Models;
using Xunit;

namespace PokemonTypeEffectiveness.Tests
{
    public class PokemonServiceTests
    {
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly HttpClient _httpClient;
        private readonly PokemonService _pokemonService;

        public PokemonServiceTests()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://pokeapi.co/api/v2/")
            };
            _pokemonService = new PokemonService(_httpClient);
        }

        [Fact]
        public async Task GetPokemonTypeInfoAsync_ReturnsCorrectStrengthsAndWeaknesses()
        {
            // Arrange: Mock a successful response for the pokemon API
            var pokemonName = "pikachu";
            var pokemonResponse = new PokemonResponse
            {
                Types = new List<TypeEntry>
                {
                    new TypeEntry { Type = new TypeDetail { Name = "electric", Url = "https://pokeapi.co/api/v2/type/13/" } }
                }
            };

            var typeResponse = new TypeResponse
            {
                Damage_Relations = new DamageRelations
                {
                    Double_Damage_To = new List<TypeDetail> { new TypeDetail { Name = "water" } },
                    Double_Damage_From = new List<TypeDetail> { new TypeDetail { Name = "ground" } },
                    Half_Damage_To = new List<TypeDetail> { new TypeDetail { Name = "electric" } },
                    Half_Damage_From = new List<TypeDetail> { new TypeDetail { Name = "rock" } },
                    No_Damage_To = new List<TypeDetail> { new TypeDetail { Name = "dragon" } },
                    No_Damage_From = new List<TypeDetail> { new TypeDetail { Name = "fairy" } }
                }
            };

            // Setup the mock to return the pokemon response
            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.RequestUri.ToString().Contains("pokemon/pikachu")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(pokemonResponse))
                });

            // Setup the mock to return the type response
            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.RequestUri.ToString().Contains("type/13")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(typeResponse))
                });

            // Act
            var result = await _pokemonService.GetPokemonTypeInfoAsync(pokemonName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Strengths.Count);
            Assert.Equal(3, result.Weaknesses.Count);
        }
    }
}
