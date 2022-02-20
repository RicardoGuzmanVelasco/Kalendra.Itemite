using System.Threading.Tasks;
using Newtonsoft.Json;
using PokeApiNet;
using UnityEngine;

namespace Kalendra.Pokemite
{
    public class TestPokeApi : MonoBehaviour
    {
        readonly PokeApiClient client = new PokeApiClient();
        
        async void Start()
        {
            var pikachu = await AwaitForPikachu();

            Debug.Log(pikachu.Sprites.FrontDefault);
        }

        async Task<Pokemon> AwaitForPikachu()
        {
            return await client.GetResourceAsync<Pokemon>("pikachu");
        }
    }
}