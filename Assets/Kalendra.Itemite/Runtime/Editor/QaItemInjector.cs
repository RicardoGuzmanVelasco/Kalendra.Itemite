using Kalendra.Itemite.Runtime.Domain.Application;
using Kalendra.Itemite.Runtime.Infrastructure.Persistence;
using UnityEngine;

namespace Kalendra.Itemite.Runtime.Editor
{
    public class QaItemInjector : MonoBehaviour
    {
        [SerializeField] Item itemToInject;

        void OnValidate()
        {
            if(!itemToInject) return;
            if(!Application.isPlaying) return;

            GetComponent<IItemPresenter>().Inject(itemToInject);
        }
    }
}