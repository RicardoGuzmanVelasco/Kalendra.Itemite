using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokeApiNet;
using UnityEngine;
using Zenject;

namespace Kalendra.Pokemite.Runtime.Infrastructure.Presentation
{
    public class CandidateSlotsController : MonoBehaviour
    {
        [SerializeField] GameObject candidateSlotsContainer;

        [Inject] readonly PokeApiClientAdapter repo;
        [Inject] readonly CurrentSelectedController selectedController;

        IList<PkmnCard> cards;
        TaskCompletionSource<Pokemon> selectPokemonAwaiter;

        void Awake()
        {
            cards = candidateSlotsContainer.GetComponentsInChildren<PkmnCard>();
        }

        public async Task RandomizeRound()
        {
            await Task.WhenAll(cards.Select(RandomizeCard));
        }

        async Task RandomizeCard(PkmnCard card)
        {
            var pkmn = await repo.GetRandomPkmn();
            var sprite = await repo.GetSpriteOfPkmn(pkmn);

            card.Inject(pkmn, sprite);
        }

        public async Task WaitForSelection()
        {
            foreach(var card in cards)
                card.Selected += CardSelected;

            selectPokemonAwaiter = new TaskCompletionSource<Pokemon>();
            await selectPokemonAwaiter.Task;

            foreach(var card in cards)
                card.Selected -= CardSelected;
        }

        public async Task UpdateCurrentPkmnWithLastSelected()
        {
            var selectedPkmn = selectPokemonAwaiter.Task.Result;

            selectedController.UpdateCurrent(new PokemonVisualDto { Pkmn = selectedPkmn });

            await Task.CompletedTask;
        }

        void CardSelected(Pokemon pkmn)
        {
            selectPokemonAwaiter.SetResult(pkmn);
        }
    }
}