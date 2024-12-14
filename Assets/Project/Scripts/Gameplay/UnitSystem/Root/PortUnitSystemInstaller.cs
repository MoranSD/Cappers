using Gameplay.Panels;
using Gameplay.Player.InteractController;
using Gameplay.Ship.UnitControl;
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

        public override void AfterInitialize()
        {
            base.AfterInitialize();

            var unitExistenceControl = ServiceLocator.Get<ShipUnitExistenceControl>();
            var playerMenuInteract = ServiceLocator.Get<PlayerInteractController>();

            panelsManager = ServiceLocator.Get<PanelsManager>();
            panelsManager.RegisterPanel(unitBuyMenuView);

            unitBuySystem = new(unitExistenceControl, unitFactory, buySystemView);
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
