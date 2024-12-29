using Gameplay.Game;
using Gameplay.Panels;
using Gameplay.Player.InteractController;
using Gameplay.Ship.UnitControl;
using Gameplay.UnitSystem.Buy.Menu;
using Gameplay.UnitSystem.Buy.Menu.View;
using Gameplay.UnitSystem.Buy.View;
using Gameplay.UnitSystem.BuyMenu;
using Gameplay.UnitSystem.Upgrade;
using Gameplay.UnitSystem.Upgrade.Menu;
using Infrastructure;
using UnityEngine;

namespace Gameplay.UnitSystem.Root
{
    public class PortUnitSystemInstaller : UnitSystemInstaller
    {
        [SerializeField] private UnitBuyMenuView unitBuyMenuView;
        [SerializeField] private UnitBuySystemView buySystemView;
        [SerializeField] private UnitUpgradeMenuView upgradeMenuView;

        private UnitBuySystem unitBuySystem;
        private UnitBuyMenu unitBuyMenu;

        private UnitUpgradeSystem unitUpgradeSystem;
        private UnitUpgradeMenu unitUpgradeMenu;

        private PanelsManager panelsManager;

        public override void AfterInitialize()
        {
            base.AfterInitialize();

            var unitExistenceControl = ServiceLocator.Get<ShipUnitExistenceControl>();
            var playerMenuInteract = ServiceLocator.Get<PlayerInteractController>();
            var gameState = ServiceLocator.Get<GameState>();

            panelsManager = ServiceLocator.Get<PanelsManager>();
            panelsManager.RegisterPanel(unitBuyMenuView);
            panelsManager.RegisterPanel(upgradeMenuView);

            unitBuySystem = new(unitExistenceControl, unitFactory, buySystemView);
            unitBuyMenu = new(unitBuySystem, playerMenuInteract, unitBuyMenuView);

            unitUpgradeSystem = new(gameState);
            unitUpgradeMenu = new(unitUpgradeSystem, gameState, upgradeMenuView, playerMenuInteract);

            unitBuyMenuView.Initialize();
            unitBuySystem.Initialize();
            unitBuyMenu.Initialize();

            upgradeMenuView.Initialize();
            unitUpgradeMenu.Initialize();
        }

        public override void Dispose()
        {
            base.Dispose();

            unitBuyMenu.Dispose();
            unitBuyMenuView.Dispose();

            unitUpgradeMenu.Dispose();
            upgradeMenuView.Dispose();

            panelsManager.UnregisterPanel(unitBuyMenuView);
            panelsManager.UnregisterPanel(upgradeMenuView);
        }
    }
}
