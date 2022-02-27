using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kalendra.Pokemite.Runtime.Domain;
using PokeApiNet;
using UnityEngine;
using UnityEngine.Networking;
using Random = System.Random;

namespace Kalendra.Pokemite.Runtime.Infrastructure
{
    [UsedImplicitly]
    public class PokeApiClientRepoAdapter : IPkmnRepo, IPkmnVisualRepo
    {
        const int PokemonCount = 887;

        readonly Dictionary<int, PkmnVisualDto> cache = new Dictionary<int, PkmnVisualDto>();

        readonly PokeApiClient client = new PokeApiClient();
        readonly Random random = new Random();

        #region Data
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
        #endregion

        #region Visual
        public async Task<PkmnVisualDto> GetRandomVisualPkmn()
        {
            var id = random.Next(1, PokemonCount + 1);

            Debug.Log("Cache has " + id + ": " + cache.ContainsKey(id));

            if(cache.ContainsKey(id))
                return cache[id];

            Debug.Log("Fetching " + id);
            var fetched = await FetchVisualPkmn(id);
            Debug.Log("Fetched " + fetched.Pkmn.Name);

            cache[id] = fetched;

            Debug.Log("Now cache has " + id + ": " + cache.ContainsKey(id));

            return fetched;
        }

        async Task<PkmnVisualDto> FetchVisualPkmn(int id)
        {
            Pokemon pkmn = null;
            Sprite sprite = null;

            await Task.WhenAll(FetchPkmn(), FetchSprite());

            return new PkmnVisualDto
            {
                Pkmn = pkmn,
                Sprite = sprite
            };

            async Task FetchPkmn() => pkmn = await GetPkmn(id);
            async Task FetchSprite() => sprite = await GetSpriteOfPkmnById(id);
        }

        static async Task<Sprite> GetSpriteOfPkmnById(int id)
        {
            using var request = UnityWebRequestTexture.GetTexture(Url(id));

            var asyncOp = request.SendWebRequest();

            while(!asyncOp.isDone)
                await Task.Delay(16);

            if(request.error != null)
                throw new HttpRequestException($"Cannot request for Pkmn of id {id}: {request.error}");

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