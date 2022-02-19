using System;
using Kalendra.Itemite.Runtime.Domain.Application;
using Kalendra.Itemite.Runtime.Infrastructure.Persistence;
using TMPro;
using UnityEngine;

namespace Kalendra.Itemite.Runtime.Infrastructure.Presentation
{
    public class Item : MonoBehaviour, IItemPresenter, IItemInput
    {
        [SerializeField] Sprite defaultUnknownIcon;

        readonly IVisualItemRepo repo = new ResourcesItemRepo();

        Domain.Item attachedItem;
        SpriteRenderer icon;

        TextMeshPro[] labels;

        void Awake()
        {
            labels = GetComponentsInChildren<TextMeshPro>();
            icon = GetComponentInChildren<SpriteRenderer>();
        }

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
            transform.name = attachedItem.Name;

            foreach(var label in labels)
                label.text = attachedItem.Name;

            var foundIcon = repo.GetIconOf(attachedItem.Name);
            icon.sprite = foundIcon ? foundIcon : defaultUnknownIcon;
        }

        public Domain.Item ToDomain()
        {
            return attachedItem;
        }
    }
}