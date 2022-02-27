using System.Threading.Tasks;
using Kalendra.Pokemite.Runtime.Domain;
using Kalendra.Pokemite.Runtime.Infrastructure.Presentation;
using PokeApiNet;
using UnityEngine;
using Zenject;

namespace Kalendra.Pokemite.Runtime.Infrastructure
{
    public class CurrentSelectedController : MonoBehaviour
    {
        [SerializeField] PkmnCard card;

        [Inject] readonly IPkmnVisualRepo repo;

        public Pokemon Pkmn => card.Pkmn;

        public async Task RandomizeFirst()
        {
            await UpdateCurrent(await repo.GetRandomVisualPkmn());
        }

        public Task UpdateCurrent(PkmnVisualDto dto)
        {
            card.Inject(dto);
            return Task.CompletedTask;
        }
    }
}