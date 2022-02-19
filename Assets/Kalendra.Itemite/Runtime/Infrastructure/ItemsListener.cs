using Kalendra.Itemite.Runtime.Domain.Application;
using Kalendra.Itemite.Runtime.Infrastructure.Presentation;
using UnityEngine;

namespace Kalendra.Itemite.Runtime.Infrastructure
{
    public class ItemsListener : MonoBehaviour
    {
        void Awake()
        {
            FindObjectOfType<ItemSpawner>().ItemSpawned += ListenToItem;
        }

        static void ListenToItem(IItemInput item)
        {
            item.Clicked += FindObjectOfType<ChainListener>().SwapItemSelection;
        }
    }
}