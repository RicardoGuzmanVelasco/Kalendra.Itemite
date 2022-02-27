using System.Linq;
using System.Threading.Tasks;
using Kalendra.Pokemite.Runtime.Domain;
using Kalendra.Pokemite.Runtime.Infrastructure.Presentation;
using PokeApiNet;
using UnityEngine;
using Zenject;

namespace Kalendra.Pokemite.Runtime.Infrastructure
{
    public class GameLoopController : MonoBehaviour
    {
        [Inject] readonly AudioSource audioPlayer;
        [Inject] readonly CandidateSlotsController candidatesController;
        [Inject] readonly CurrentSelectedController currentController;
        [Inject] readonly ResultController resultController;

        async void Start()
        {
            await SetFirstPokemon();

            do
            {
                await PlayRound();
            } while(true);
        }

        async Task PlayRound()
        {
            await candidatesController.RandomizeRound();

            var selected = await candidatesController.WaitForSelection();

            await candidatesController.ShowCardScores(currentController.CurrentPkmn);
            resultController.UpdateScore(ChoiceFrom(selected));
            currentController.UpdateCurrent(selected);
        }

        Choice ChoiceFrom(PkmnVisualDto selected)
        {
            var current = currentController.CurrentPkmn;
            var selectedCandidate = selected.Pkmn;
            var allCandidates = candidatesController.Cards.Select(c => c.Pkmn);

            var wasGoodChoice = Relate(current, selectedCandidate) >= allCandidates.Max(p => Relate(current, p));

            return new Choice
            {
                Pkmn = selected,
                IsBest = wasGoodChoice
            };
        }

        static float Relate(Pokemon source, Pokemon with)
        {
            return
                new RelatablePkmn(source)
                    .RelateWith(
                        new RelatablePkmn(with));
        }

        #region SettingUp
        async Task SetFirstPokemon()
        {
            await currentController.RandomizeFirst();
        }
        #endregion
    }
}