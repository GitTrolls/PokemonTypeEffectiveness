namespace PokemonTypeEffectiveness.Models
{
    public class DamageRelations
    {
        public List<TypeDetail>? Double_Damage_To { get; set; }
        public List<TypeDetail>? Double_Damage_From { get; set; }
        public List<TypeDetail>? Half_Damage_To { get; set; }
        public List<TypeDetail>? Half_Damage_From { get; set; }
        public List<TypeDetail>? No_Damage_To { get; set; }
        public List<TypeDetail>? No_Damage_From { get; set; }
    }
}
