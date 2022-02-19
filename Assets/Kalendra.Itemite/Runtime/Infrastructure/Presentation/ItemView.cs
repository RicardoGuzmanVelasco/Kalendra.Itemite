using System;
using Kalendra.Itemite.Runtime.Domain.Application;
using TMPro;
using UnityEngine;

namespace Kalendra.Itemite.Runtime.Infrastructure.Presentation
{
    public class ItemView : MonoBehaviour, IItemPresenter, IItemInput
    {
        [SerializeField] TextMeshPro label;

        Domain.Item attachedItem;

        public void OnMouseUpAsButton()
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