using System.Net.Http;
using System.Threading.Tasks;
using Kalendra.Pokemite.Runtime.Domain;
using PokeApiNet;
using UnityEngine;
using UnityEngine.Networking;
using Random = System.Random;

namespace Kalendra.Pokemite.Runtime.Infrastructure
{
    public class PokeApiClientAdapter : IPkmnRepo, IPkmnVisualRepo
    {
        const int PokemonCount = 887;

        readonly PokeApiClient client = new PokeApiClient();
        readonly Random random = new Random();

        public async Task<Pokemon> GetRandomPkmn()
        {
            return await GetPkmn(random.Next(1, PokemonCount + 1));
        }

        public async Task<Pokemon> GetPkmn(string name)
        {
            return await client.GetResourceAsync<Pokemon>(name);
        }

        public async Task<Pokemon> GetPkmn(int id)
        {
            return await client.GetResourceAsync<Pokemon>(id);
        }

        #region Visual
        public async Task<PkmnVisualDto> GetRandomVisualPkmn()
        {
            var id = random.Next(1, PokemonCount + 1);

            Pokemon pkmn;
            pkmn = await GetPkmn(id);

            Sprite sprite;
            sprite = await GetSpriteOfPkmnById(id);

            return new PkmnVisualDto
            {
                Pkmn = pkmn,
                Sprite = sprite
            };
        }

        public async Task<Sprite> GetSpriteOfPkmnById(int id)
        {
            using var request = UnityWebRequestTexture.GetTexture(Url(id));

            var asyncOp = request.SendWebRequest();

            while(!asyncOp.isDone)
                await Task.Delay(16);

            if(request.error != null)
                throw new HttpRequestException(request.error);

            var texture = DownloadHandlerTexture.GetContent(request);

            return SpriteFrom(texture);
        }

        static string Url(int id)
        {
            return $"https://cdn.traction.one/pokedex/pokemon/{id}.png";
        }

        static Sprite SpriteFrom(Texture2D texture)
        {
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * .5f, 100f);
        }
        #endregion
    }
}