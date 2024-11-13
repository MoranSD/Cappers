using Gameplay.UnitSystem.Factory;
using Infrastructure;
using Infrastructure.Composition;
using Infrastructure.DataProviding;
using Leopotam.Ecs;

namespace Gameplay.UnitSystem.Root
{
    public class UnitSystemInstaller : Installer
    {
        protected UnitFactory unitFactory;

        public override void PostInitialize()
        {
            var assetProvider = ServiceLocator.Get<IAssetProvider>();
            var ecsWorld = ServiceLocator.Get<EcsWorld>();

            var factoryConfig = assetProvider.Load<UnitFactoryConfig>(Constants.UnitFactoryConfig);
            unitFactory = new UnitFactory(ecsWorld, factoryConfig);
            ServiceLocator.Register<IUnitFactory>(unitFactory);
        }

        public override void Dispose()
        {
            ServiceLocator.Remove<IUnitFactory>();
        }
    }
}
