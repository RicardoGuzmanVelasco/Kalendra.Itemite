using Kalendra.Itemite.Runtime.Infrastructure.Presentation;
using UnityEngine;

namespace Kalendra.Itemite.Runtime.Infrastructure
{
    public class SelectedChainLogger : MonoBehaviour
    {
        void Start()
        {
            FindObjectOfType<Chain>().SelectedChainChanged += Debug.Log;
        }
    }
}