using Kalendra.Pokemite.Runtime.Infrastructure;
using Kalendra.Pokemite.Runtime.Infrastructure.Presentation;
using Zenject;

namespace Kalendra.Pokemite.Runtime.EntryPoint
{
    public class SceneDependencyResolver : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PokeApiClientAdapter>().AsSingle();

            Container.Bind<CurrentSelectedController>().FromInstance(FindObjectOfType<CurrentSelectedController>());
            Container.Bind<CandidateSlotsController>().FromInstance(FindObjectOfType<CandidateSlotsController>());
            Container.Bind<ResultController>().FromInstance(FindObjectOfType<ResultController>());
        }
    }
}