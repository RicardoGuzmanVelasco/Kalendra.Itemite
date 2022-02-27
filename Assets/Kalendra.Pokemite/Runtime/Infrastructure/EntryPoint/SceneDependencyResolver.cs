using Kalendra.Pokemite.Runtime.Infrastructure;
using Kalendra.Pokemite.Runtime.Infrastructure.Presentation;
using UnityEngine;
using Zenject;

namespace Kalendra.Pokemite.Runtime.EntryPoint
{
    public class SceneDependencyResolver : MonoInstaller
    {
        public override void InstallBindings()
        {
            ByInterfacesAndSelfTo<PokeApiClientAdapter>().AsSingle();

            ByFindingSingleInScene<CurrentSelectedController>();
            ByFindingSingleInScene<CandidateSlotsController>();
            ByFindingSingleInScene<ResultController>();

            ByComponentInScene<AudioSource>();
        }

        FromBinderNonGeneric ByInterfacesAndSelfTo<T>()
        {
            return Container.BindInterfacesAndSelfTo<T>();
        }

        void ByFindingSingleInScene<T>() where T : Object
        {
            Container.Bind<T>().FromInstance(FindObjectOfType<T>());
        }

        void ByComponentInScene<T>() { }
    }
}