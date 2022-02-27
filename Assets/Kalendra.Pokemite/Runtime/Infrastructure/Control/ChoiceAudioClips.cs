using System;
using UnityEngine;

namespace Kalendra.Pokemite.Runtime.Infrastructure
{
    [Serializable]
    public class ChoiceAudioClips
    {
        [SerializeField] AudioClip GoodChoice;
        [SerializeField] AudioClip BadChoice;

        public AudioClip ByResult(bool wasGoodChoice)
        {
            return wasGoodChoice ? GoodChoice : BadChoice;
        }
    }
}