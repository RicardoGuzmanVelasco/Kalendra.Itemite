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
            Debug.Log("Start");

            Debug.Log("Getting first selected");
            await currentController.RandomizeFirst();
            Debug.Log("First selected ready");

            Debug.Log("Randomizing round");
            await candidatesController.RandomizeRound();
            Debug.Log("Round ready");

            Debug.Log("Choose!");
            await candidatesController.WaitForSelection();
            Debug.Log("Choice ready");
        }
    }
}