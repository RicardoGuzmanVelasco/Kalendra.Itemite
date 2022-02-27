using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kalendra.Pokemite.Runtime.Domain;
using PokeApiNet;
using UnityEngine;
using Zenject;

namespace Kalendra.Pokemite.Runtime.Infrastructure.Presentation
{
    public class CandidateSlotsController : MonoBehaviour
    {
        [SerializeField] GameObject candidateSlotsContainer;
        [SerializeField] AudioClip selectedClip;
        [Inject] readonly AudioSource audioPlayer;
        [Inject] readonly IPkmnVisualRepo repo;

        TaskCompletionSource<PkmnVisualDto> selectPokemonAwaiter;

        public IEnumerable<PkmnCard> Cards { get; private set; }

        void Awake()
        {
            Cards = candidateSlotsContainer.GetComponentsInChildren<PkmnCard>();
        }

        public async Task RandomizeRound()
        {
            await Task.WhenAll(Cards.Select(RandomizeCard));
        }

        async Task RandomizeCard(PkmnCard card)
        {
            var pkmn = await repo.GetRandomVisualPkmn();
            card.Inject(pkmn);
        }

        public async Task<PkmnVisualDto> WaitForSelection()
        {
            foreach(var card in Cards)
                card.Selected += CardSelected;

            selectPokemonAwaiter = new TaskCompletionSource<PkmnVisualDto>();
            await selectPokemonAwaiter.Task;

            foreach(var card in Cards)
                card.Selected -= CardSelected;

            return selectPokemonAwaiter.Task.Result;
        }

        void CardSelected(PkmnVisualDto pkmn)
        {
            audioPlayer.PlayOneShot(selectedClip);
            selectPokemonAwaiter.SetResult(pkmn);
        }

        public async Task ShowCardScores(Pokemon currentPkmn)
        {
            var relations = Cards.Select(card => Relate(currentPkmn, card.Pkmn)).ToList();

            await Task.WhenAll
            (
                Cards.Select
                ((card, i) =>
                    card.GetComponentInParent<PkmnCandidateSlot>()
                        .AnimateResult((int)relations[i], relations[i] >= relations.Max())
                )
            );
        }

        static float Relate(Pokemon currentPkmn, Pokemon cardPkmn)
        {
            return
                new RelatablePkmn(currentPkmn)
                    .RelateWith(
                        new RelatablePkmn(cardPkmn));
        }

        public Task FreeCards()
        {
            foreach(var card in Cards)
                card.Discard();
            return Task.CompletedTask;
        }
    }
}