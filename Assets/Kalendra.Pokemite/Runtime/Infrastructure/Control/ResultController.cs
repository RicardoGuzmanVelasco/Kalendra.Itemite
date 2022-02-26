using Kalendra.Itemite.Runtime.Infrastructure;
using Kalendra.Pokemite.Runtime.Domain;
using Kalendra.Pokemite.Runtime.Infrastructure.Presentation;
using PokeApiNet;
using UnityEngine;

namespace Kalendra.Pokemite.Runtime.Infrastructure
{
    public class ResultController : MonoBehaviour
    {
        [SerializeField] PkmnCard currentPkmnCard;
        [SerializeField] OutlinedLabel resultLabel;

        float accResult;

        public void ComputeNewChoice(PkmnVisualDto choice)
        {
            var points = Relate(currentPkmnCard.Contained, choice.Pkmn);

            accResult += points;
            resultLabel.Text = ((int)accResult).ToString();
        }

        static float Relate(Pokemon source, Pokemon with)
        {
            return
                new RelatablePkmn(source)
                    .RelateWith(
                        new RelatablePkmn(with));
        }
    }
}