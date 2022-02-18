using UnityEngine;

namespace Kalendra.Itemite.Runtime.Infrastructure
{
    [CreateAssetMenu(fileName = "New Tag", menuName = "Kalendra/Itemite/Tag", order = 0)]
    public sealed class Tag : ScriptableObject
    {
        public string Id => name;

        bool Equals(Tag other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object other)
        {
            if(ReferenceEquals(null, other)) return false;
            if(ReferenceEquals(this, other)) return true;
            if(other.GetType() != GetType()) return false;
            return Equals((Tag)other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}