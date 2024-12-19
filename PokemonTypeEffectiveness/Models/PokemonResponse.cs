namespace PokemonTypeEffectiveness.Models
{
    public class PokemonResponse
    {
        public List<TypeEntry> Types { get; set; }
    }

    public class TypeEntry
    {
        public TypeDetail Type { get; set; }
    }
}
