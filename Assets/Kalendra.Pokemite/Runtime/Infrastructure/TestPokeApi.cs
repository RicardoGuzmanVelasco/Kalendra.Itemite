using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;
using Kalendra.Itemite.Runtime.Infrastructure;
using PokeApiNet;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Kalendra.Pokemite.Infrastructure
{
    public class TestPokeApi : MonoBehaviour
    {
        readonly PokeApiClientAdapter repo = new PokeApiClientAdapter();

        async void Start()
        {
            var pkmn = await AwaitForRandomPkmn();

            //StartCoroutine(LoadSprite(pikachu.Sprites.FrontDefault));

            StartCoroutine(LoadSprite($"https://cdn.traction.one/pokedex/pokemon/{pkmn.Id}.png"));
            FindObjectOfType<OutlinedLabel>().Text = pkmn.Name;
        }

        IEnumerator LoadSprite(string url)
        {
            using var request = UnityWebRequestTexture.GetTexture(url);

            yield return request.SendWebRequest();
            if(request.error != null)
                throw new HttpRequestException(request.error);

            var texture = DownloadHandlerTexture.GetContent(request);

            FindObjectOfType<Image>().sprite = SpriteFrom(texture);
        }

        Sprite SpriteFrom(Texture2D texture)
        {
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * .5f, 100f);
        }

        async Task<Pokemon> AwaitForRandomPkmn()
        {
            return await repo.GetRandomPkmn();
        }
    }
}