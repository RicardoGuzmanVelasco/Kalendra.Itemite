using PokeApiNet;

namespace Kalendra.Pokemite.Runtime.Domain
{
    public interface IPkmnRelation
    {
        float Relate(Pokemon p1, Pokemon p2);
    }
}