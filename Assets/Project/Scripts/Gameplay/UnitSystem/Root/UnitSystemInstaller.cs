using Gameplay.Game;
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

        public override void PreInitialize()
        {
            var assetProvider = ServiceLocator.Get<IAssetProvider>();
            var ecsWorld = ServiceLocator.Get<EcsWorld>();

            var factoryConfig = assetProvider.Load<UnitFactoryConfig>(Constants.UnitFactoryConfig);
            var gameConfig = assetProvider.Load<GameConfig>(Constants.GameConfigPath);
            unitFactory = new UnitFactory(ecsWorld, factoryConfig, gameConfig);
            ServiceLocator.Register<IUnitFactory>(unitFactory);
        }

        public override void Dispose()
        {
            ServiceLocator.Remove<IUnitFactory>();
        }
    }
}
