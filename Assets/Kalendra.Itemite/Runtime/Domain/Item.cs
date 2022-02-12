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
            var allTags = Tags.Union(other.Tags);
            var commonTags = Tags.Intersect(other.Tags);

            return 100f * commonTags.Count() / allTags.Count();
        }

        public Chain ChainWith(Item item)
        {
            return new Chain(this, item);
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
            if(ReferenceEquals(null, obj)) return false;
            if(ReferenceEquals(this, obj)) return true;
            if(obj.GetType() != this.GetType()) return false;
            return Equals((Item)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Tags);
        }
        #endregion
    }
}