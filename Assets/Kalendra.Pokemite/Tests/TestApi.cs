using System.Threading.Tasks;
using Kalendra.Pokemite.Runtime.Domain;
using Kalendra.Pokemite.Runtime.Infrastructure;
using PokeApiNet;

namespace Kalendra.Pokemite.Tests
{
    internal static class TestApi
    {
        static readonly IPkmnRepo Repo = new PokeApiClientRepoAdapter();

        public static Task<Pokemon> Bulbasaur => AskFor("bulbasaur");
        public static Task<Pokemon> Squirtle => AskFor("squirtle");
        public static Task<Pokemon> Wartortle => AskFor("wartortle");
        public static Task<Pokemon> Graveler => AskFor("graveler");
        public static Task<Pokemon> Sunflora => AskFor("sunflora");
        public static Task<Pokemon> Torterra => AskFor("torterra");
        public static Task<Pokemon> Amaura => AskFor("amaura");
        public static Task<Pokemon> Aurorus => AskFor("aurorus");

        static async Task<Pokemon> AskFor(string name)
        {
            return await Repo.GetPkmn(name);
        }
    }
}