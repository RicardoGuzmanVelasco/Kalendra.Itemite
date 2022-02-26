using System.Threading.Tasks;
using PokeApiNet;

namespace Kalendra.Pokemite.Runtime.Domain
{
    public interface IPkmnRepo
    {
        Task<Pokemon> GetRandomPkmn();
        Task<Pokemon> GetPkmn(string name);
        Task<Pokemon> GetPkmn(int id);
    }
}