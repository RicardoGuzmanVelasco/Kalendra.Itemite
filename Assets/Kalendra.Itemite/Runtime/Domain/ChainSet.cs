using System.Collections.Generic;
using System.Linq;

namespace Kalendra.Itemite.Runtime.Domain
{
    public class ChainSet
    {
        readonly IEnumerable<Item> items;

        IEnumerable<Chain> orderedChains;

        public ChainSet(IEnumerable<Item> items)
        {
            this.items = items;
        }

        public Chain BestChain()
        {
            return OrderChains().First();
        }

        /// <remarks> Warning: No CQRS! </remarks>
        public IEnumerable<Chain> OrderChains()
        {
            orderedChains ??= Permutation.Of(items)
                .Select(chain => new Chain(chain))
                .OrderByDescending(c => c.Reduce());
            return orderedChains;
        }

        public override string ToString()
        {
            OrderChains();

            return $"Max: {BestChain().Reduce()} for items {FormatItems()}";

            string FormatItems()
            {
                return items.Aggregate("", (current, item) => current + $"{item} ");
            }
        }
    }
}