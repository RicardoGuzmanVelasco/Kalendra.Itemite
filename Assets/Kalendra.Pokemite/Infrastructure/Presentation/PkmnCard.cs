using Kalendra.Itemite.Runtime.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace Kalendra.Pokemite.Infrastructure.Presentation
{
    public class PkmnCard : MonoBehaviour
    {
        ILabel label;
        Image picture;

        void Awake()
        {
            picture = GetComponentInChildren<Image>();
            label = GetComponentInChildren<OutlinedLabel>();
        }

        public void Inject(string pkmnName, Sprite pkmnSprite)
        {
            label.Text = pkmnName;
            picture.sprite = pkmnSprite;
        }
    }
}