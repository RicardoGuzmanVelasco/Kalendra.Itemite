using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Kalendra.Itemite.Runtime.Domain
{
    public sealed class Item : IChainable
    {
        public Item(string name, string tags) : this(name, tags.Split(',')) { }

        public Item(string name, [NotNull] ICollection<string> tags)
        {
            if(!tags.Any())
                throw new ArgumentException();

            Name = name;
            Tags = new HashSet<string>(tags.Select(s => s.Trim()));
        }

        public string Name { get; }
        public HashSet<string> Tags { get; }

        public override string ToString()
        {
            return Name;
        }

        #region Public API
        public float RelateWith(Item other)
        {
            return Relate(this, other);
        }

        public Chain ChainWith(Item item)
        {
            return new Chain(this, item);
        }
        #endregion

        #region Relation strategies
        public static readonly Func<Item, Item, float> CommonProportion = (it1, it2) =>
        {
            return 100f
                   * CommonTags(it1, it2).Count()
                   / AllTags(it1, it2).Count();
        };

        public static readonly Func<Item, Item, float> FirstItemDeviation = (it1, it2) =>
        {
            return 100f * CommonTags(it1, it2).Count()
                   - AllTags(it1, it2)
                       .Except(CommonTags(it1, it2))
                       .Intersect(it1.Tags).Count();
        };

        static float Relate(Item it1, Item it2)
        {
            return RelationStrategy(it1, it2);
        }

        public static Func<Item, Item, float> RelationStrategy { get; set; } = CommonProportion;

        static IEnumerable<string> AllTags(Item it1, Item it2)
        {
            return it1.Tags.Union(it2.Tags);
        }

        static IEnumerable<string> CommonTags(Item it1, Item it2)
        {
            return it1.Tags.Intersect(it2.Tags);
        }
        #endregion

        #region Equality
        bool Equals(Item other)
        {
            return Name == other.Name &&
                   Tags.Count == other.Tags.Count &&
                   Tags.All(other.Tags.Contains);
        }

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj))
                return false;
            if(ReferenceEquals(this, obj))
                return true;
            return obj.GetType() == GetType() && Equals((Item)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Tags);
        }
        #endregion
    }
}