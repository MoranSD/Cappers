using Gameplay.Panels;
using Gameplay.Player.InteractController;
using Gameplay.Ship.UnitControl.Placement;
using Gameplay.UnitSystem.Buy.Menu;
using Gameplay.UnitSystem.Buy.Menu.View;
using Gameplay.UnitSystem.Buy.View;
using Gameplay.UnitSystem.BuyMenu;
using Gameplay.UnitSystem.Factory;
using Infrastructure;
using Infrastructure.Composition;
using Infrastructure.DataProviding;
using Infrastructure.TickManagement;
using UnityEngine;

namespace Gameplay.UnitSystem.Root
{
    public class UnitSystemInstaller : Installer
    {
        [SerializeField] private UnitBuyMenuView unitBuyMenuView;
        [SerializeField] private UnitBuySystemView buySystemView;

        private UnitFactory unitFactory;
        private UnitBuySystem unitBuySystem;
        private UnitBuyMenu unitBuyMenu;

        private PanelsManager panelsManager;

        public override void Initialize()
        {
            panelsManager = ServiceLocator.Get<PanelsManager>();
            panelsManager.RegisterPanel(unitBuyMenuView);

            var assetProvider = ServiceLocator.Get<IAssetProvider>();
            var tickManager = ServiceLocator.Get<TickManager>();
            var unitPlacement = ServiceLocator.Get<ShipUnitPlacement>();
            var playerMenuInteract = ServiceLocator.Get<PlayerMenuInteractController>();

            var factoryConfig = assetProvider.Load<UnitFactoryConfig>(Constants.UnitFactoryConfig);
            unitFactory = new UnitFactory(tickManager, factoryConfig);

            unitBuySystem = new(unitPlacement, unitFactory, buySystemView);
            unitBuyMenu = new(unitBuySystem, playerMenuInteract, unitBuyMenuView);

            unitBuyMenuView.Initialize();
            unitBuySystem.Initialize();
            unitBuyMenu.Initialize();
        }

        public override void Dispose()
        {
            unitFactory.Dispose();

            unitBuyMenu.Dispose();
            unitBuyMenuView.Dispose();

            panelsManager.UnregisterPanel(unitBuyMenuView);
        }
    }
}
