using Kalendra.Pokemite.Runtime.Domain;
using Newtonsoft.Json;
using PokeApiNet;
using UnityEngine;

namespace Kalendra.Pokemite.Runtime.Infrastructure
{
    [CreateAssetMenu(fileName = "New Pkmn", menuName = "Kalendra/Pokemite/PkmnDto", order = 0)]
    public class PkmnFakeDto : ScriptableObject
    {
        [SerializeField, Multiline] string json;
        [SerializeField] Sprite sprite;

        public static implicit operator PkmnVisualDto(PkmnFakeDto fake) => new PkmnVisualDto
        {
            Pkmn = JsonConvert.DeserializeObject<Pokemon>(fake.json),
            Sprite = fake.sprite
        };
    }
}