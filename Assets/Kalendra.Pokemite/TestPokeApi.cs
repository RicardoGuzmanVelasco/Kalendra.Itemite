using System.Collections;
using System.Threading.Tasks;
using PokeApiNet;
using UnityEngine;
using UnityEngine.Networking;

namespace Kalendra.Pokemite
{
    public class TestPokeApi : MonoBehaviour
    {
        readonly PokeApiClient client = new PokeApiClient();

        async void Start()
        {
            var pikachu = await AwaitForPikachu();

            StartCoroutine(LoadSprite(pikachu.Sprites.FrontDefault));
        }

        IEnumerator LoadSprite(string url)
        {
            using var request = UnityWebRequestTexture.GetTexture(url);

            yield return request.SendWebRequest();

            var texture = DownloadHandlerTexture.GetContent(request);

            // render.sprite = SpriteFrom(texture);
        }

        Sprite SpriteFrom(Texture2D texture)
        {
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * .5f, 100f);
        }

        async Task<Pokemon> AwaitForPikachu()
        {
            return await client.GetResourceAsync<Pokemon>("pikachu");
        }
    }
}