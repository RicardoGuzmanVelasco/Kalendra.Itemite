using Kalendra.Itemite.Runtime.Domain.Application;
using UnityEngine;

namespace Kalendra.Itemite.Runtime.Infrastructure.Presentation
{
    public class QaItemInjector : MonoBehaviour
    {
        [SerializeField] Persistence.Item itemToInject;

        void OnValidate()
        {
            if(!itemToInject) return;
            if(!Application.isPlaying) return;

            GetComponent<IItemPresenter>().Inject(itemToInject);
        }
    }
}