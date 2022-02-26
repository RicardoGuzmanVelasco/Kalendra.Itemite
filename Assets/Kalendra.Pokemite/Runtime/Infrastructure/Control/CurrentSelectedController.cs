using System.Threading.Tasks;
using Kalendra.Pokemite.Runtime.Infrastructure.Presentation;
using PokeApiNet;
using UnityEngine;

namespace Kalendra.Pokemite.Runtime.Infrastructure
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

        public void UpdateCurrent(PokemonVisualDto dto)
        {
            card.Inject(dto.Pkmn, dto.Sprite);
        }
    }

    public class PokemonVisualDto
    {
        public Pokemon Pkmn { get; set; }
        public Sprite Sprite { get; set; }
    }
}