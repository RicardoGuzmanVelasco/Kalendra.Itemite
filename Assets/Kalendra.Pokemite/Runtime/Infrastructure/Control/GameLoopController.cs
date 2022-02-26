using System.Threading.Tasks;
using Kalendra.Pokemite.Runtime.Infrastructure.Presentation;
using UnityEngine;
using Zenject;

namespace Kalendra.Pokemite.Runtime.Infrastructure
{
    public class GameLoopController : MonoBehaviour
    {
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
            resultController.UpdateScore(selected);
            currentController.UpdateCurrent(selected);
        }

        async Task SetFirstPokemon()
        {
            await currentController.RandomizeFirst();
        }
    }
}