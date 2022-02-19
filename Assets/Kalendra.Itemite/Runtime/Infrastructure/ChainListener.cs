using System.Collections.Generic;
using Kalendra.Itemite.Runtime.Domain;
using UnityEngine;

namespace Kalendra.Itemite.Runtime.Infrastructure
{
    public class ChainListener : MonoBehaviour
    {
        readonly List<Domain.Item> selectedChain = new List<Domain.Item>();

        public void SwapItemSelection(Domain.Item item)
        {
            if(selectedChain.Contains(item))
                selectedChain.Clear();
            else
                selectedChain.Add(item);

            Debug.Log(new Chain(selectedChain));
        }
    }
}