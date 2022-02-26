using System.Threading.Tasks;
using Kalendra.Pokemite.Infrastructure.Presentation;
using UnityEngine;

namespace Kalendra.Pokemite.Infrastructure
{
    public class CurrentSelectedController : MonoBehaviour
    {
        [SerializeField] PkmnCard card;

        readonly PokeApiClientAdapter repo = new PokeApiClientAdapter();

        public async Task RandomizeFirst()
        {
            var pkmn = await repo.GetRandomPkmn();
            var sprite = await repo.GetSpriteOfPkmn(pkmn);

            card.Inject(pkmn, sprite);
        }
    }
}