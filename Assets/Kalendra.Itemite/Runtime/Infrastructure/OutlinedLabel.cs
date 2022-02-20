using TMPro;
using UnityEngine;

namespace Kalendra.Itemite.Runtime.Infrastructure
{
    public class OutlinedLabel : MonoBehaviour, ILabel
    {
        [SerializeField] TMP_Text outline;
        [SerializeField] TMP_Text content;

        public string Text
        {
            get => outline.text;
            set
            {
                outline.text = value;
                content.text = value;
            }
        }

        public Color Color
        {
            get => content.color;
            set
            {
                content.color = value;
                outline.color = outline.color.With(a: value.a);
            }
        }
    }
}