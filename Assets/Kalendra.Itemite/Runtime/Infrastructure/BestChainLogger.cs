using Kalendra.Itemite.Runtime.Infrastructure.Presentation;
using UnityEngine;

namespace Kalendra.Itemite.Runtime.Infrastructure
{
    public class BestChainLogger : MonoBehaviour
    {
        void Start()
        {
            FindObjectOfType<ChainSeeker>().BestChainFound += Debug.Log;
        }
    }
}