using System;
using Kalendra.Itemite.Runtime.Domain.Application;
using Kalendra.Itemite.Runtime.Infrastructure.Persistence;
using TMPro;
using UnityEngine;

namespace Kalendra.Itemite.Runtime.Infrastructure.Presentation
{
    public class Item : MonoBehaviour, IItemPresenter
    {
        [SerializeField] Sprite defaultUnknownIcon;

        readonly IVisualItemRepo repo = new ResourcesItemRepo();
        Domain.Item attachedItem;
        SpriteRenderer icon;
        TextMeshPro[] labels;

        bool selected;

        void Awake()
        {
            labels = GetComponentsInChildren<TextMeshPro>();
            icon = GetComponentInChildren<SpriteRenderer>();
        }

        void OnMouseUp()
        {
            if(attachedItem is null)
                return;

            Clicked.Invoke(this);
        }

        public bool Selected
        {
            get => selected;
            set
            {
                selected = value;
                icon.color = icon.color.With(selected ? .75f : 1f);
            }
        }

        public event Action<Item> Clicked = _ => { };

        public Domain.Item ToDomain()
        {
            return attachedItem;
        }

        #region IItemPresenter implementation
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
        #endregion
    }
}