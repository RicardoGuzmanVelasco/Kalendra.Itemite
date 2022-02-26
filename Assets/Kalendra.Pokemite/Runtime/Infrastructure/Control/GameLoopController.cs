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
            Debug.Log("Randomizing round");
            await candidatesController.RandomizeRound();
            Debug.Log("Round ready");

            Debug.Log("Choose!");
            var selected = await candidatesController.WaitForSelection();
            Debug.Log("Choice ready");

            resultController.ComputeNewChoice(selected);
            currentController.UpdateCurrent(selected);
        }

        async Task SetFirstPokemon()
        {
            Debug.Log("Start");

            Debug.Log("Getting first selected");
            await currentController.RandomizeFirst();
            Debug.Log("First selected ready");
        }
    }
}