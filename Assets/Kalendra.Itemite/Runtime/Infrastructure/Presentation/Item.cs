using System;
using Kalendra.Itemite.Runtime.Domain.Application;
using Kalendra.Itemite.Runtime.Infrastructure.Persistence;
using TMPro;
using UnityEngine;

namespace Kalendra.Itemite.Runtime.Infrastructure.Presentation
{
    public class Item : MonoBehaviour, IItemPresenter, IItemInput
    {
        [SerializeField] TextMeshPro label;

        readonly IVisualItemRepo repo = new ResourcesItemRepo();

        Domain.Item attachedItem;

        void OnMouseUp()
        {
            if(attachedItem is null)
                return;

            Clicked.Invoke(attachedItem);
        }

        public event Action<Domain.Item> Clicked = _ => { };


        public void Inject(Domain.Item item)
        {
            attachedItem = item;
            DrawItem();
        }

        void DrawItem()
        {
            label.text = attachedItem.Name;
            transform.name = attachedItem.Name;
        }
    }
}