using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Kalendra.Itemite.Runtime.Domain
{
    public partial class Item : IChainable 
    {
        public string Name { get; }
        public IEnumerable<string> Tags { get; }

        public Item(string name, [NotNull] IEnumerable<string> tags)
        {
            if(!tags.Any())
                throw new ArgumentException();

            Name = name;
            Tags = new HashSet<string>(tags);
        }

        public float RelateWith(Item other)
        {
            var allTags = Tags.Union(other.Tags);
            var commonTags = Tags.Intersect(other.Tags);
            
            return commonTags.Count()/(float)allTags.Count();
        }

        public Chain ChainWith(Item item)
        {
            return new Chain(this, item);
        }
    }
}