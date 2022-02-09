using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalendra.Itemite.Runtime.Domain
{
    public sealed class Chain : IChainable
    {
        readonly List<Item> items = new List<Item>();

        public Chain(IEnumerable<Item> startingOrderedItems)
            : this(startingOrderedItems.ToArray()) { }
            
        public Chain(params Item[] startingOrderedItems)
        {
            foreach(var item in startingOrderedItems)
                Add(item);
        }

        #region Public API
        public Chain ChainWith(Item item)
        {
            Add(item);
            return this;
        }

        public float Reduce()
        {
            var result = 0f;

            for(var i = 0; i < items.Count - 1; i++)
                result += items[i].RelateWith(items[i + 1]);
            
            return result;
        }
        #endregion

        #region SupportMethods
        void Add(Item item)
        {
            if(items.Contains(item))
                throw new ArgumentException($"{item} is already chained");
                
            items.Add(item);
        }
        #endregion

        #region Equality
        bool Equals(Chain other)
        {
            if(items.Count != other.items.Count)
                return false;

            return !items.Where((item, i) => !item.Equals(other.items[i])).Any();
        }

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj)) return false;
            if(ReferenceEquals(this, obj)) return true;
            if(obj.GetType() != this.GetType()) return false;
            return Equals((Chain)obj);
        }

        public override int GetHashCode()
        {
            return (items != null ? items.GetHashCode() : 0);
        }
        #endregion
        
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