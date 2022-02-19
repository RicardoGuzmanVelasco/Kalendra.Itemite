using System.Collections.Generic;
using System.Linq;
using Kalendra.Itemite.Runtime.Infrastructure.Presentation;
using UnityEngine;

namespace Kalendra.Itemite.Runtime.Infrastructure
{
    public class Chain : MonoBehaviour
    {
        readonly List<Item> selectedItems = new List<Item>();

        void Awake()
        {
            FindObjectOfType<ItemSpawner>().ItemSpawned += ListenToItem;

            void ListenToItem(Item item)
            {
                item.Clicked += SwapItemSelection;
            }
        }

        public void SwapItemSelection(Item item)
        {
            if(selectedItems.Contains(item))
                DeselectAll();
            else
                Select(item);

            Debug.Log(new Domain.Chain(selectedItems.Select(i => i.ToDomain())));

            void DeselectAll()
            {
                foreach(var item in selectedItems)
                    item.Selected = false;

                selectedItems.Clear();
            }

            void Select(Item i)
            {
                i.Selected = true;
                selectedItems.Add(i);
            }
        }
    }
}