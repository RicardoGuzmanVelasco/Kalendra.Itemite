using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalendra.Itemite.Runtime.Domain
{
    public class Chain : IChainable
    {
        readonly List<Item> items = new List<Item>();

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
            if(items.Contains(item))
                throw new ArgumentException($"{item} is already chained");
                
            items.Add(item);
        }

        public float Reduce()
        {
            var result = 0f;

            for(var i = 0; i < items.Count - 1; i++)
                result += items[i].RelateWith(items[i + 1]);
            
            return result;
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            FormatReductionAsHeader();
            FormatRelationsAsBody();

            return result.ToString();
            
            
            void FormatRelationsAsBody()
            {
                for(var i = 0; i < items.Count; i++)
                    result.Append(items[i]).Append(FormatRelationAt(i));
            }
            string FormatRelationAt(int i)
            {
                if(items[i] == items.Last())
                    return string.Empty;

                var relation = items[i].RelateWith(items[i + 1]);
                return $" -{relation:0.}-> ";
            }

            void FormatReductionAsHeader()
            {
                result.Append($"[{Reduce():0.##}] | ");
            }
        }
    }
}