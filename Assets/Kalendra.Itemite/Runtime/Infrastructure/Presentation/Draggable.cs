using UnityEngine;

namespace Kalendra.Itemite.Runtime.Infrastructure.Presentation
{
    public class Draggable : MonoBehaviour
    {
        void OnMouseDrag()
        {
            transform.position = PointerInWorld().With(z: transform.position.z);
        }

        static Vector3 PointerInWorld()
        {
            return Camera.main!.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}