using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Kalendra.Pokemite.Infrastructure.Presentation
{
    public class CandidateSlotsController : MonoBehaviour
    {
        readonly PokeApiClientAdapter repo = new PokeApiClientAdapter();

        IEnumerable<PkmnCard> cards;

        void Awake()
        {
            cards = FindObjectsOfType<PkmnCard>();
        }

        void Start()
        {
            foreach(var card in cards)
                card.gameObject.SetActive(false);
        }

        public async Task RandomizeRound()
        {
            foreach(var card in cards)
                card.gameObject.SetActive(true);

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