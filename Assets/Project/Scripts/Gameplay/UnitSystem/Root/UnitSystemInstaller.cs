using Gameplay.Panels;
using Gameplay.Player.InteractController;
using Gameplay.Ship.UnitControl.Placement;
using Gameplay.UnitSystem.Buy.Menu;
using Gameplay.UnitSystem.Buy.Menu.View;
using Gameplay.UnitSystem.BuyMenu;
using Gameplay.UnitSystem.Factory;
using Infrastructure;
using Infrastructure.Composition;
using UnityEngine;

namespace Gameplay.UnitSystem.Root
{
    public class UnitSystemInstaller : Installer
    {
        [SerializeField] private UnitBuyMenuView unitBuyMenuView;

        private UnitBuySystem unitBuySystem;
        private UnitBuyMenu unitBuyMenu;

        private PanelsManager panelsManager;

        public override void Initialize()
        {
            panelsManager = ServiceLocator.Get<PanelsManager>();
            panelsManager.RegisterPanel(unitBuyMenuView);

            var unitPlacement = ServiceLocator.Get<ShipUnitPlacement>();
            var playerMenuInteract = ServiceLocator.Get<PlayerMenuInteractController>();

            var unitFactory = new UnitFactory();

            unitBuySystem = new(unitPlacement, unitFactory);
            unitBuyMenu = new(unitBuySystem, playerMenuInteract, unitBuyMenuView);

            unitBuyMenuView.Initialize();
            unitBuySystem.Initialize();
            unitBuyMenu.Initialize();
        }

        public override void Dispose()
        {
            unitBuyMenu.Dispose();
            unitBuyMenuView.Dispose();

            panelsManager.UnregisterPanel(unitBuyMenuView);
        }
    }
}
