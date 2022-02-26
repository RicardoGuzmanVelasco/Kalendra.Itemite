using System.Threading.Tasks;
using Kalendra.Pokemite.Runtime.Domain;
using Kalendra.Pokemite.Runtime.Infrastructure.Presentation;
using UnityEngine;

namespace Kalendra.Pokemite.Runtime.Infrastructure
{
    public class CurrentSelectedController : MonoBehaviour
    {
        [SerializeField] PkmnCard card;

        readonly PokeApiClientAdapter repo = new PokeApiClientAdapter();

        public async Task RandomizeFirst()
        {
            UpdateCurrent(await repo.GetRandomVisualPkmn());
        }

        public void UpdateCurrent(PkmnVisualDto dto)
        {
            card.Inject(dto);
        }
    }
}