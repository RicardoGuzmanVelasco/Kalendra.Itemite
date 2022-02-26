using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokeApiNet;
using UnityEngine;

namespace Kalendra.Pokemite.Infrastructure.Presentation
{
    public class CandidateSlotsController : MonoBehaviour
    {
        //TODO: use when ContinueWithInMainThread it's available! SetActive() does not work when on threading.
        [SerializeField] GameObject candidateSlotsContainer;

        readonly PokeApiClientAdapter repo = new PokeApiClientAdapter();

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

        void CardSelected(Pokemon pkmn)
        {
            selectPokemonAwaiter.SetResult(pkmn);
        }
    }
}