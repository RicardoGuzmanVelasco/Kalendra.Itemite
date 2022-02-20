using System.Linq;
using Kalendra.Itemite.Runtime.Infrastructure.Persistence;
using Kalendra.Itemite.Runtime.Infrastructure.Presentation;
using UnityEngine;
using Zenject;

namespace Kalendra.Itemite.Runtime.Infrastructure.Main
{
    public class EntryPointInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ResourcesItemRepo>().AsSingle().Lazy();

            BindFromScene<Chain>();
            BindFromScene<ChainSeeker>();
            BindFromScene<ItemSpawner>();
        }

        void BindFromScene<T>() where T : Object
        {
            Container.Bind<T>().FromInstance(FindObjectsOfType<T>().Single()).AsCached();
        }
    }
}