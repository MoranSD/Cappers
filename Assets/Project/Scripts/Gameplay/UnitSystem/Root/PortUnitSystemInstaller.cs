using Gameplay.Panels;
using Gameplay.Player.InteractController;
using Gameplay.Ship.UnitControl.Placement;
using Gameplay.UnitSystem.Buy.Menu;
using Gameplay.UnitSystem.Buy.Menu.View;
using Gameplay.UnitSystem.Buy.View;
using Gameplay.UnitSystem.BuyMenu;
using Infrastructure;
using UnityEngine;

namespace Gameplay.UnitSystem.Root
{
    public class PortUnitSystemInstaller : UnitSystemInstaller
    {
        [SerializeField] private UnitBuyMenuView unitBuyMenuView;
        [SerializeField] private UnitBuySystemView buySystemView;

        private UnitBuySystem unitBuySystem;
        private UnitBuyMenu unitBuyMenu;

        private PanelsManager panelsManager;

        public override void PostInitialize()
        {
            base.PostInitialize();

            var unitPlacement = ServiceLocator.Get<ShipUnitPlacement>();
            var playerMenuInteract = ServiceLocator.Get<PlayerMenuInteractController>();

            panelsManager = ServiceLocator.Get<PanelsManager>();
            panelsManager.RegisterPanel(unitBuyMenuView);

            unitBuySystem = new(unitPlacement, unitFactory, buySystemView);
            unitBuyMenu = new(unitBuySystem, playerMenuInteract, unitBuyMenuView);

            unitBuyMenuView.Initialize();
            unitBuySystem.Initialize();
            unitBuyMenu.Initialize();
        }

        public override void Dispose()
        {
            base.Dispose();

            unitBuyMenu.Dispose();
            unitBuyMenuView.Dispose();

            panelsManager.UnregisterPanel(unitBuyMenuView);
        }
    }
}
