using System.Threading.Tasks;
using Kalendra.Pokemite.Runtime.Domain;
using Kalendra.Pokemite.Runtime.Infrastructure;
using PokeApiNet;

namespace Kalendra.Pokemite.Tests
{
    internal static class TestApi
    {
        static readonly IPkmnRepo Repo = new PokeApiClientRepoAdapter();

        public static async Task<Pokemon> For(string name)
        {
            return await Repo.GetPkmn(name);
        }
    }
}