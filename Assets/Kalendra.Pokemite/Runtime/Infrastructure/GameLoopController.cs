using System.Threading.Tasks;
using Kalendra.Pokemite.Infrastructure.Presentation;
using UnityEngine;

namespace Kalendra.Pokemite.Infrastructure
{
    public class GameLoopController : MonoBehaviour
    {
        CandidateSlotsController candidatesController;
        CurrentSelectedController currentController;

        void Awake()
        {
            currentController = FindObjectOfType<CurrentSelectedController>();
            candidatesController = FindObjectOfType<CandidateSlotsController>();
        }

        async void Start()
        {
            await SetFirstPokemon();

            do
            {
                await PlayRounds();
            } while(true);
        }

        async Task PlayRounds()
        {
            Debug.Log("Randomizing round");
            await candidatesController.RandomizeRound();
            Debug.Log("Round ready");

            Debug.Log("Choose!");
            await candidatesController.WaitForSelection();
            Debug.Log("Choice ready");
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