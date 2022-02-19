using System;
using System.Collections;
using System.Linq;
using Kalendra.Itemite.Runtime.Domain;
using UnityEngine;

namespace Kalendra.Itemite.Runtime.Infrastructure.Presentation
{
    public class ChainSeeker : MonoBehaviour
    {
        IEnumerator Start()
        {
            yield return new WaitForSeconds(2f);

            var allSceneItems = FindObjectsOfType<Item>();

            var chainSet = new ChainSet(allSceneItems.Select(i => i.ToDomain()));
            BestChainFound.Invoke(chainSet.BestChain());
        }

        public event Action<Domain.Chain> BestChainFound = _ => { };
    }
}