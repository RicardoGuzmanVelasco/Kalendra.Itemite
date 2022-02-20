using Kalendra.Pokemite.Domain;
using PokeApiNet;

namespace Kalendra.Pokemite
{
    public class RelatablePkmn : IRelatable<RelatablePkmn>
    {
        readonly Pokemon pkmn;

        public RelatablePkmn(Pokemon pkmn)
        {
            this.pkmn = pkmn;
        }

        public float RelateWith(RelatablePkmn other)
        {
            return RelationHeuristic(this, other);
        }

        static float RelationHeuristic(RelatablePkmn pkmn1, RelatablePkmn pkmn2)
        {
            return pkmn1.pkmn.Height - pkmn2.pkmn.Height;
        }
    }
}