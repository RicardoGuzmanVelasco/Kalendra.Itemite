using System.Threading.Tasks;
using PokeApiNet;
using UnityEngine;

namespace Kalendra.Pokemite.Domain
{
    public interface IPkmnVisualRepo
    {
        public Task<Sprite> GetSpriteOfPkmn(Pokemon pkmn);
    }
}