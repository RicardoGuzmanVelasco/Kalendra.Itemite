using Kalendra.Itemite.Runtime.Infrastructure.Presentation;
using UnityEngine;
using Zenject;

namespace Kalendra.Itemite.Runtime.Infrastructure
{
    public class BestChainLogger : MonoBehaviour
    {
        [Inject] readonly ChainSeeker seeker;

        void Start()
        {
            seeker.BestChainFound += Debug.Log;
        }
    }
}