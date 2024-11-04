using Gameplay.UnitSystem.Factory;
using Infrastructure;
using Infrastructure.Composition;
using Infrastructure.DataProviding;
using Infrastructure.TickManagement;

namespace Gameplay.UnitSystem.Root
{
    public class UnitSystemInstaller : Installer
    {
        protected UnitFactory unitFactory;

        public override void Initialize()
        {
            var assetProvider = ServiceLocator.Get<IAssetProvider>();
            var tickManager = ServiceLocator.Get<TickManager>();

            var factoryConfig = assetProvider.Load<UnitFactoryConfig>(Constants.UnitFactoryConfig);
            unitFactory = new UnitFactory(tickManager, factoryConfig);
            ServiceLocator.Register<IUnitFactory>(unitFactory);
        }

        public override void Dispose()
        {
            ServiceLocator.Remove<IUnitFactory>();
            unitFactory.Dispose();
        }
    }
}
