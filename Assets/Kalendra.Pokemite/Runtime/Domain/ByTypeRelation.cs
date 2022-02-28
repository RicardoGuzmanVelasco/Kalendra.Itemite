using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using PokeApiNet;

namespace Kalendra.Pokemite.Runtime.Domain
{
    public class ByTypeRelation : IPkmnRelation
    {
        public float Relate(Pokemon p1, Pokemon p2)
        {
            var types1 = TypeNamesFrom(p1);
            var types2 = TypeNamesFrom(p2);

            var common = CommonTypes(types1, types2);
            var unCommon = UncommonTypes(types1, types2);

            return common / (unCommon + 1f);
        }

        [Pure]
        static int UncommonTypes(IEnumerable<string> types1, IEnumerable<string> types2)
        {
            return types1.Union(types2).Count() - CommonTypes(types1, types2);
        }

        [Pure]
        static int CommonTypes(IEnumerable<string> types1, IEnumerable<string> types2)
        {
            return types1.Intersect(types2).Count();
        }

        [Pure]
        static IEnumerable<string> TypeNamesFrom(Pokemon pkmn)
        {
            return pkmn.Types.Select(t => t.Type.Name);
        }
    }
}