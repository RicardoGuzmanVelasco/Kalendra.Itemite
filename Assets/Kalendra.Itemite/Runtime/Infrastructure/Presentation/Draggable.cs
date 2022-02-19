using UnityEngine;

// ReSharper disable Unity.InefficientPropertyAccess

namespace Kalendra.Itemite.Runtime.Infrastructure.Presentation
{
    public class Draggable : MonoBehaviour
    {
        Vector3? offset;

        void OnMouseDrag()
        {
            offset ??= transform.position - PointerInWorld().With(z: transform.position.z);
            transform.position = PointerInWorld().With(z: transform.position.z) + offset.Value;
        }

        void OnMouseUp()
        {
            offset = null;
        }

        static Vector3 PointerInWorld()
        {
            return Camera.main!.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}