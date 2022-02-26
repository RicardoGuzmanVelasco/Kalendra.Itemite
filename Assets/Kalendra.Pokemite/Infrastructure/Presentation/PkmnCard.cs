using DG.Tweening;
using Kalendra.Itemite.Runtime.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace Kalendra.Pokemite.Infrastructure.Presentation
{
    public class PkmnCard : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        ILabel label;
        Image picture;

        bool IsHidden => canvasGroup.alpha == 0;

        void Awake()
        {
            picture = GetComponentInChildren<Image>();
            label = GetComponentInChildren<OutlinedLabel>();

            canvasGroup = GetComponent<CanvasGroup>();
        }

        void Start()
        {
            Hide();
        }

        void Hide()
        {
            if(IsHidden)
                return;

            canvasGroup.alpha = 0;
        }

        public void Inject(string pkmnName, Sprite pkmnSprite)
        {
            label.Text = pkmnName;
            picture.sprite = pkmnSprite;

            if(IsHidden)
                ShowAnimation();
        }

        void ShowAnimation()
        {
            DOTween.To
            (
                () => canvasGroup.alpha,
                value => canvasGroup.alpha = value,
                1f,
                .25f
            );
        }
    }
}