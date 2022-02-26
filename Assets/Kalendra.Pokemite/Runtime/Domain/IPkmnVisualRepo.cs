using System.Threading.Tasks;

namespace Kalendra.Pokemite.Runtime.Domain
{
    public interface IPkmnVisualRepo
    {
        public Task<PkmnVisualDto> GetRandomVisualPkmn();
    }
}