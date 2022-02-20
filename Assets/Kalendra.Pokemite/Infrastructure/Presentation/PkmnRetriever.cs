using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Kalendra.Pokemite.Infrastructure.Presentation
{
    public class PkmnRetriever : MonoBehaviour
    {
        readonly PokeApiClientAdapter repo = new PokeApiClientAdapter();

        IList<PkmnCard> cards;

        void Awake()
        {
            cards = FindObjectsOfType<PkmnCard>();
        }

        async void Start()
        {
            await RandomizeRound();
        }

        async Task RandomizeRound()
        {
            await Task.WhenAll(cards.Select(RandomizeCard));
        }

        async Task RandomizeCard(PkmnCard card)
        {
            var pkmn = await repo.GetRandomPkmn();
            var sprite = await repo.GetSpriteOfPkmn(pkmn);

            card.Inject(pkmn.Name, sprite);
        }
    }
}