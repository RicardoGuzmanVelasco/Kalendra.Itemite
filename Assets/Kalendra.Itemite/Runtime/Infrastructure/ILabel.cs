using UnityEngine;

namespace Kalendra.Itemite.Runtime.Infrastructure
{
    public interface ILabel
    {
        string Text { get; set; }
        Color Color { get; set; }
    }
}