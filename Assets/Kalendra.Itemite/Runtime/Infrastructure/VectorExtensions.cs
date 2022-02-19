using UnityEngine;

namespace Kalendra.Itemite.Runtime.Infrastructure
{
    public static class VectorExtensions
    {
        public static Vector3 With(this Vector3 source, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? source.x, y ?? source.y, z ?? source.z);
        }
    }
}