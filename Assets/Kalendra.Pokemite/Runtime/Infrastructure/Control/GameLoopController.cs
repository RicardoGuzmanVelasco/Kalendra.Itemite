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
        [Inject] readonly CandidateSlotsController candidates;
        [Inject] readonly CurrentSelectedController current;
        [Inject] readonly ResultController score;

        async void Start()
        {
            await SetFirstPokemon();

            do await PlayRound();
            while(true);
        }

        async Task PlayRound()
        {
            await UpdateRoundCounter();

            await candidates.RandomizeRound();

            var selected = await candidates.WaitForSelection();

            await candidates.ShowCardScores(current.Pkmn);
            await score.UpdateScore(ChoiceFrom(selected));
            await current.UpdateCurrent(selected);

            await candidates.FreeCards();
        }

        Task UpdateRoundCounter()
        {
            Debug.Log("New Round!");
            return Task.Delay(1000);
        }

        #region Support methods
        Choice ChoiceFrom(PkmnVisualDto selected)
        {
            var current = this.current.Pkmn;
            var selectedCandidate = selected.Pkmn;
            var allCandidates = candidates.Cards.Select(c => c.Pkmn);

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

        async Task SetFirstPokemon()
        {
            await current.RandomizeFirst();
        }
        #endregion
    }
}