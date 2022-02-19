using System.Collections;
using System.Linq;
using Kalendra.Itemite.Runtime.Domain;
using UnityEngine;
using Item = Kalendra.Itemite.Runtime.Infrastructure.Presentation.Item;

namespace Kalendra.Itemite.Runtime.Infrastructure
{
    public class ChainSeeker : MonoBehaviour
    {
        IEnumerator Start()
        {
            yield return new WaitForSeconds(2f);

            var allSceneItems = FindObjectsOfType<Item>();

            var chainSet = new ChainSet(allSceneItems.Select(i => i.ToDomain()));
            Debug.Log(chainSet.BestChain());
        }
    }
}