using System;
using System.Threading.Tasks;
using PokeApiNet;

namespace Kalendra.Pokemite.Infrastructure
{
    public class PkmnRepo
    {
        const int PokemonCount = 898;

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
    }
}