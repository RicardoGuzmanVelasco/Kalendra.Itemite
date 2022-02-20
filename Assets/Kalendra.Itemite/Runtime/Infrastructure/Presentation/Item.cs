using System;
using DG.Tweening;
using Kalendra.Itemite.Runtime.Domain.Application;
using Kalendra.Itemite.Runtime.Infrastructure.Persistence;
using UnityEngine;

namespace Kalendra.Itemite.Runtime.Infrastructure.Presentation
{
    public class Item : MonoBehaviour, IItemPresenter
    {
        [SerializeField] Sprite defaultUnknownIcon;

        readonly IVisualItemRepo repo = new ResourcesItemRepo();
        Domain.Item attachedItem;

        SpriteRenderer icon;
        ILabel label;

        bool selected;

        void Awake()
        {
            icon = GetComponentInChildren<SpriteRenderer>();
            label = GetComponentInChildren<ILabel>();
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
                ToggleSelectedTween();
                ToggleSelectedText();
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
            label.Text = attachedItem.Name;

            var foundIcon = repo.GetIconOf(attachedItem.Name);
            icon.sprite = foundIcon ? foundIcon : defaultUnknownIcon;
        }
        #endregion

        #region Animation
        void ToggleSelectedTween()
        {
            if(selected)
                transform
                    .DOShakePosition(
                        .5f,
                        .025f,
                        50,
                        fadeOut: false)
                    .SetEase(Ease.Linear)
                    .SetLoops(-1);
            else
                transform.DOKill();
        }

        void ToggleSelectedText()
        {
            label.Color = selected &&
                          ColorUtility.TryParseHtmlString("#10FF4D", out var color)
                ? color
                : Color.white;
        }
        #endregion
    }
}