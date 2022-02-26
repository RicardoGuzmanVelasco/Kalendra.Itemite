using UnityEngine;
using UnityEngine.UI;

namespace Kalendra.Pokemite.Infrastructure.Presentation
{
    [RequireComponent(typeof(RawImage))]
    public class ParallaxRawImage : MonoBehaviour
    {
        [SerializeField] Vector2 speed = Vector2.one;

        RawImage image;

        void Awake()
        {
            image = GetComponent<RawImage>();
        }

        void Update()
        {
            image.uvRect = new Rect
            (
                speed * Time.deltaTime + image.uvRect.position,
                image.uvRect.size
            );
        }
    }
}