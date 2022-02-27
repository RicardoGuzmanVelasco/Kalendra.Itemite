using System.Linq;
using PokeApiNet;

namespace Kalendra.Pokemite.Runtime.Domain
{
    public class ByTypeRelation : IPkmnRelation
    {
        public float Relate(Pokemon p1, Pokemon p2)
        {
            var types1 = p1.Types.Select(t => t.Type.Name);
            var types2 = p2.Types.Select(t => t.Type.Name);

            var common = types1.Intersect(types2).Count();
            var unCommon = types1.Union(types2).Count() - common;

            return (float)common / (unCommon > 0 ? unCommon : 1);
        }
    }
}