using System.Linq;
using UnityEngine;

namespace Kalendra.Itemite.Runtime.Infrastructure.Persistence
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Kalendra/Itemite/Item", order = 0)]
    public sealed class Item : ScriptableObject
    {
        [SerializeField] Tag[] tags;
        [SerializeField] Sprite icon;

        public string Id => name;
        public Sprite Icon => icon;

        public Domain.Item ToDomain()
        {
            return new Domain.Item(Id, tags.Select(t => t.Id).ToList());
        }

        bool Equals(Item other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object other)
        {
            if(ReferenceEquals(null, other)) return false;
            if(ReferenceEquals(this, other)) return true;
            if(other.GetType() != GetType()) return false;
            return Equals((Item)other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static implicit operator Domain.Item(Item item)
        {
            return item.ToDomain();
        }
    }
}