using System;
using System.Linq;
using System.Numerics;
using PokeApiNet;

namespace Kalendra.Pokemite.Runtime.Domain
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
            return RelationHeuristic(this.pkmn, other.pkmn);
        }

        static float RelationHeuristic(Pokemon p1, Pokemon p2)
        {
            return ByBaseExp(p1, p2) + ByTypes(p1, p2);
        }

        static float ByBaseExp(Pokemon p1, Pokemon p2)
        {
            var baseExpRange = new Vector2(20, 255);
            var baseExpDifference = Math.Abs(p1.BaseExperience - p2.BaseExperience);

            return baseExpRange.Length() - baseExpDifference;
        }

        static float ByTypes(Pokemon p1, Pokemon p2)
        {
            const int typesCount = 18;
            return p1.Types.Intersect(p2.Types).Count() * typesCount;
            //TODO: bonus if slots match (slots = type is primary, secondary...)
        }
    }
}