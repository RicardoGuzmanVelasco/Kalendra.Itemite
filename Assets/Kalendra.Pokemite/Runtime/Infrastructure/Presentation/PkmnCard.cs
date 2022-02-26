using System;
using DG.Tweening;
using Kalendra.Itemite.Runtime.Infrastructure;
using PokeApiNet;
using UnityEngine;
using UnityEngine.UI;

namespace Kalendra.Pokemite.Runtime.Infrastructure.Presentation
{
    public class PkmnCard : MonoBehaviour
    {
        CanvasGroup canvasGroup;

        Pokemon containedPokemon;
        ILabel label;

        float originalAlpha = 1f;
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

        public event Action<PokemonVisualDto> Selected = _ => Debug.Log("PkmnCard: Selected: " + _.Pkmn.Name);

        void Hide()
        {
            if(IsHidden)
                return;

            originalAlpha = canvasGroup.alpha;
            canvasGroup.alpha = 0;
        }

        public void Inject(Pokemon pkmn, Sprite pkmnSprite)
        {
            containedPokemon = pkmn;
            label.Text = pkmn.Name;
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
                originalAlpha,
                .25f
            );
        }

        public void OnClick()
        {
            var dto = new PokemonVisualDto
            {
                Pkmn = containedPokemon,
                Sprite = picture.sprite
            };
            Selected.Invoke(dto);
        }
    }
}