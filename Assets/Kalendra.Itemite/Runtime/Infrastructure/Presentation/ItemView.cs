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

        public void OnMouseDown()
        {
            Clicked.Invoke();
        }

        public event Action Clicked = () => { };

        void IItemPresenter.Inject(Domain.Item item)
        {
            attachedItem = item;
            DrawItem();
        }

        void DrawItem()
        {
            label.text = attachedItem.Name;
        }
    }
}