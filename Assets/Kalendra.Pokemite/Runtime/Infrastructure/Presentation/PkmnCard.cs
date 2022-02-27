using System;
using DG.Tweening;
using Kalendra.Itemite.Runtime.Infrastructure;
using Kalendra.Pokemite.Runtime.Domain;
using PokeApiNet;
using UnityEngine;
using UnityEngine.UI;

namespace Kalendra.Pokemite.Runtime.Infrastructure.Presentation
{
    public class PkmnCard : MonoBehaviour
    {
        CanvasGroup canvasGroup;

        ILabel label;

        float originalAlpha = 1f;
        Image picture;

        public Pokemon Pkmn { get; private set; }
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

        public event Action<PkmnVisualDto> Selected = _ => { };

        public void Inject(PkmnVisualDto dto)
        {
            Pkmn = dto.Pkmn;
            label.Text = dto.Pkmn.Name;
            picture.sprite = dto.Sprite;

            if(IsHidden)
                ShowAnimation();
        }

        void ShowAnimation()
        {
            DOTween.To
            (
                () => canvasGroup.alpha,
                value => canvasGroup.alpha = value,
                originalAlpha,
                .25f
            );
        }

        public void Discard()
        {
            DOTween.To
            (
                () => canvasGroup.alpha,
                value => canvasGroup.alpha = value,
                0,
                .25f
            );
        }

        void Hide()
        {
            if(IsHidden)
                return;

            originalAlpha = canvasGroup.alpha;
            canvasGroup.alpha = 0;
        }

        public void OnClick()
        {
            var dto = new PkmnVisualDto
            {
                Pkmn = Pkmn,
                Sprite = picture.sprite
            };
            Selected.Invoke(dto);
        }
    }
}