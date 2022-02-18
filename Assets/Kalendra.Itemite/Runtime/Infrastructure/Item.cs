using System.Linq;
using UnityEngine;

namespace Kalendra.Itemite.Runtime.Infrastructure
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Kalendra/Itemite/Item", order = 0)]
    public sealed class Item : ScriptableObject
    {
        [SerializeField] Tag[] tags;

        string Id => name;

        public Domain.Item ToDomain()
        {
            return new Domain.Item(Id, tags.Select(t => t.Id).ToList());
        }

        protected bool Equals(Item other)
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
    }
}