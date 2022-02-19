using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kalendra.Itemite.Runtime.Infrastructure.Presentation
{
    public class Chain : MonoBehaviour
    {
        readonly List<Item> selectedItems = new List<Item>();
        Domain.Chain CurrentChain => new Domain.Chain(selectedItems.Select(item => item.ToDomain()));

        void Awake()
        {
            FindObjectOfType<ItemSpawner>().ItemSpawned += ListenToItem;

            void ListenToItem(Item item)
            {
                item.Clicked += SwapItemSelection;
            }
        }

        public event Action<Domain.Chain> SelectedChainChanged = _ => { };

        public void SwapItemSelection(Item item)
        {
            if(selectedItems.Contains(item))
                DeselectAll();
            else
                Select(item);

            SelectedChainChanged.Invoke(CurrentChain);
        }

        void DeselectAll()
        {
            foreach(var item in selectedItems)
                item.Selected = false;

            selectedItems.Clear();
        }

        void Select(Item item)
        {
            item.Selected = true;
            selectedItems.Add(item);
        }
    }
}