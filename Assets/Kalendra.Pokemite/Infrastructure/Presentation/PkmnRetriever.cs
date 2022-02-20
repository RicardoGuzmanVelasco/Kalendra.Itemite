using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokeApiNet;
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
            await InjectInCard(await repo.GetRandomPkmn(), cards.First());
            await InjectInCard(await repo.GetRandomPkmn(), cards.Last());
        }

        async Task InjectInCard(Pokemon pkmn, PkmnCard card)
        {
            card.Inject(pkmn.Name, await repo.GetSpriteOfPkmn(pkmn));
        }
    }
}