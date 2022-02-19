using System.Collections.Generic;
using Kalendra.Itemite.Runtime.Infrastructure.Presentation;
using UnityEngine;
using Item = Kalendra.Itemite.Runtime.Domain.Item;

namespace Kalendra.Itemite.Runtime.Infrastructure
{
    public class Chain : MonoBehaviour
    {
        readonly List<Item> selectedItems = new List<Item>();

        void Awake()
        {
            FindObjectOfType<ItemSpawner>().ItemSpawned += ListenToItem;

            void ListenToItem(Presentation.Item item)
            {
                item.Clicked += SwapItemSelection;
            }
        }

        public void SwapItemSelection(Item item)
        {
            if(selectedItems.Contains(item))
                selectedItems.Clear();
            else
                selectedItems.Add(item);

            Debug.Log(new Domain.Chain(selectedItems));
        }
    }
}