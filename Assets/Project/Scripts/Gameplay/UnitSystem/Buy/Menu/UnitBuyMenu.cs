using Gameplay.Player.InteractController;
using Gameplay.UnitSystem.Buy.Menu.View;
using Gameplay.UnitSystem.BuyMenu;
using System;

namespace Gameplay.UnitSystem.Buy.Menu
{
    public class UnitBuyMenu
    {
        private readonly UnitBuySystem buySystem;
        private readonly PlayerMenuInteractController playerMenuInteract;
        private readonly IUnitBuyMenuView view;

        public UnitBuyMenu(UnitBuySystem buySystem, PlayerMenuInteractController playerMenuInteract, IUnitBuyMenuView view)
        {
            this.buySystem = buySystem;
            this.playerMenuInteract = playerMenuInteract;
            this.view = view;
        }

        public void Initialize()
        {
            view.OnPlayerInteract += OnPlayerInteract;
            view.OnTryToClose += OnTryToClose;
            view.OnSelectUnitToBuy += OnSelectUnitToBuy;
        }

        public void Dispose()
        {
            view.OnPlayerInteract -= OnPlayerInteract;
            view.OnTryToClose -= OnTryToClose;
            view.OnSelectUnitToBuy -= OnSelectUnitToBuy;
        }

        private void OnSelectUnitToBuy(int unitId)
        {
            bool success = buySystem.TryBuyUnit(unitId);
            view.DrawSoldResult(unitId, success);
        }

        private void OnPlayerInteract()
        {
            if (playerMenuInteract.CheckInteraction(Panels.PanelType.unitBuy))
                throw new Exception();

            var units = buySystem.GetUnitsInStock();
            view.DrawBuyItems(units);
            playerMenuInteract.EnterInteractState(Panels.PanelType.unitBuy);
        }

        private void OnTryToClose()
        {
            if (playerMenuInteract.CheckInteraction(Panels.PanelType.unitBuy) == false)
                throw new Exception();

            playerMenuInteract.ExitInteractState();
        }
    }
}
