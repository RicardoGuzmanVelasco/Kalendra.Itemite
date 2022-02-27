using DG.Tweening;
using Kalendra.Itemite.Runtime.Infrastructure;
using Kalendra.Pokemite.Runtime.Domain;
using Kalendra.Pokemite.Runtime.Infrastructure.Presentation;
using PokeApiNet;
using UnityEngine;
using Zenject;

namespace Kalendra.Pokemite.Runtime.Infrastructure
{
    public class ResultController : MonoBehaviour
    {
        [SerializeField] PkmnCard currentPkmnCard;
        [SerializeField] OutlinedLabel resultLabel;
        [SerializeField] ChoiceAudioClips choiceClips;

        float accResult;

        [Inject] AudioSource audioPlayer;

        public void UpdateScore(Choice choice)
        {
            UpdateScore(choice.Pkmn);
            PlayChoiceAudio(choice.IsBest);
        }

        void UpdateScore(PkmnVisualDto choice)
        {
            var points = Relate(currentPkmnCard.Pkmn, choice.Pkmn);

            AnimateNewPoints(accResult, points);
            accResult += points;
        }

        static float Relate(Pokemon source, Pokemon with)
        {
            return
                new RelatablePkmn(source)
                    .RelateWith(
                        new RelatablePkmn(with));
        }

        #region Animation
        void AnimateNewPoints(float currentPoints, float addingPoints)
        {
            AnimatePointsNumber(currentPoints, addingPoints);
            AnimateResultBalance(addingPoints);
        }

        void AnimatePointsNumber(float currentPoints, float addingPoints)
        {
            DOTween.To
            (
                () => currentPoints,
                value => { resultLabel.Text = value.ToString("00000."); },
                addingPoints,
                1.5f
            );
        }

        void AnimateResultBalance(float deltaPoints)
        {
            switch(deltaPoints)
            {
                case 0:
                    return;
                case > 0:
                    resultLabel.transform.DOShakeScale(1.5f, .1f, 1);
                    break;
                case < 0:
                    resultLabel.transform.DOShakeRotation(1.5f);
                    break;
            }
        }

        void PlayChoiceAudio(bool wasGoodChoice)
        {
            audioPlayer.PlayOneShot(choiceClips.ByResult(wasGoodChoice));
        }
        #endregion
    }
}