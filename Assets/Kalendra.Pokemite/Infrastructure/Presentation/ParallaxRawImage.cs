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
                new Vector2
                (
                    speed.x * Time.deltaTime + image.uvRect.x,
                    speed.y * Time.deltaTime + image.uvRect.y),
                new Vector2(image.uvRect.width, image.uvRect.height
                )
            );
        }
    }
}