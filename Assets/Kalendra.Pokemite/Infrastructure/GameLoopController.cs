using Kalendra.Pokemite.Infrastructure.Presentation;
using UnityEngine;

namespace Kalendra.Pokemite.Infrastructure
{
    public class GameLoopController : MonoBehaviour
    {
        CandidateSlotsController retriever;

        void Awake()
        {
            retriever = FindObjectOfType<CandidateSlotsController>();
        }

        async void Start()
        {
            Debug.Log("Start");
            Debug.Log("Retrieving");
            await retriever.RandomizeRound();
            Debug.Log("Retrieved!");
        }
    }
}