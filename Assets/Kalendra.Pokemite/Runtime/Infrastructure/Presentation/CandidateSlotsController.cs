using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kalendra.Pokemite.Runtime.Domain;
using UnityEngine;
using Zenject;

namespace Kalendra.Pokemite.Runtime.Infrastructure.Presentation
{
    public class CandidateSlotsController : MonoBehaviour
    {
        [SerializeField] GameObject candidateSlotsContainer;
        [Inject] readonly PokeApiClientAdapter repo;

        IEnumerable<PkmnCard> cards;

        TaskCompletionSource<PkmnVisualDto> selectPokemonAwaiter;
        IEnumerable<PkmnCandidateSlot> slots;

        void Awake()
        {
            cards = candidateSlotsContainer.GetComponentsInChildren<PkmnCard>();
            slots = cards.Select(c => c.GetComponentInParent<PkmnCandidateSlot>());
        }

        public async Task RandomizeRound()
        {
            await Task.WhenAll(cards.Select(RandomizeCard));
        }

        async Task RandomizeCard(PkmnCard card)
        {
            var pkmn = await repo.GetRandomVisualPkmn();
            card.Inject(pkmn);
        }

        public async Task<PkmnVisualDto> WaitForSelection()
        {
            foreach(var card in cards)
                card.Selected += CardSelected;

            selectPokemonAwaiter = new TaskCompletionSource<PkmnVisualDto>();
            await selectPokemonAwaiter.Task;

            foreach(var card in cards)
                card.Selected -= CardSelected;

            return selectPokemonAwaiter.Task.Result;
        }

        void CardSelected(PkmnVisualDto pkmn)
        {
            selectPokemonAwaiter.SetResult(pkmn);
        }

        public async Task ShowChoice(PkmnVisualDto selectedChoice)
        {
            await Task.WhenAll(slots.Select(s => s.AnimateResult(selectedChoice)));
        }
    }
}