using UnityEngine;

namespace Kalendra.Itemite.Runtime.Infrastructure.Presentation
{
    public interface IVisualItemRepo
    {
        Sprite GetIconOf(string itemId);
    }
}