using System;
using System.Collections.Generic;
using System.Linq;

namespace Kalendra.Itemite.Runtime.Domain
{
    public partial class Item
    {
        public class Chain : IChainable
        {
            readonly List<Item> chainedInOrder = new List<Item>();

            public Chain(IEnumerable<Item> startingOrderedItems)
                : this(startingOrderedItems.ToArray()) { }
            
            public Chain(params Item[] startingOrderedItems)
            {
                foreach(var item in startingOrderedItems)
                    Add(item);
            }

            public Chain ChainWith(Item item)
            {
                Add(item);
                return this;
            }

            void Add(Item item)
            {
                if(chainedInOrder.Contains(item))
                    throw new ArgumentException($"{item} is already chained");
                
                chainedInOrder.Add(item);
            }
        }
    }
}