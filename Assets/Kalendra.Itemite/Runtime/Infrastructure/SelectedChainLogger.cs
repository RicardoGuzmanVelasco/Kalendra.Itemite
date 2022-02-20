using Kalendra.Itemite.Runtime.Infrastructure.Presentation;
using UnityEngine;
using Zenject;

namespace Kalendra.Itemite.Runtime.Infrastructure
{
    public class SelectedChainLogger : MonoBehaviour
    {
        [Inject] readonly Chain chain;

        void Start()
        {
            chain.SelectedChainChanged += Debug.Log;
        }
    }
}