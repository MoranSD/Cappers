using Gameplay.Game;
using Gameplay.Player.InteractController;
using Gameplay.UnitSystem.Data;
using System;

namespace Gameplay.UnitSystem.Upgrade.Menu
{
    public class UnitUpgradeMenu
    {
        private readonly UnitUpgradeSystem upgradeSystem;
        private readonly GameState gameState;
        private readonly IUnitUpgradeMenuView view;
        private readonly PlayerInteractController playerInteract;

        public UnitUpgradeMenu(UnitUpgradeSystem upgradeSystem, GameState gameState, IUnitUpgradeMenuView view, PlayerInteractController playerInteract)
        {
            this.upgradeSystem = upgradeSystem;
            this.gameState = gameState;
            this.view = view;
            this.playerInteract = playerInteract;
        }

        public void Initialize()
        {
            view.OnPlayerInteract += OnOpenMenu;
            view.OnTryToClose += OnTryToClose;
            view.OnHeal += OnTryHeal;
            view.OnUpgrade += OnTryUpgrade;
        }

        public void Dispose()
        {
            view.OnPlayerInteract -= OnOpenMenu;
            view.OnTryToClose -= OnTryToClose;
            view.OnHeal -= OnTryHeal;
            view.OnUpgrade -= OnTryUpgrade;
        }

        private void OnOpenMenu()
        {
            if (playerInteract.CheckInteraction(Panels.PanelType.unitUpgrade))
                throw new Exception();

            view.ClearItems();

            for (int i = 0; i < gameState.Units.Count; i++)
            {
                var unitData = gameState.Units[i];

                if(unitData.CurrentHealth != unitData.MaxHealth)
                {
                    view.DrawHealItem(new()
                    {
                        Id = unitData.Id,
                        CurrentHealth = unitData.CurrentHealth,
                        MaxHealth = unitData.MaxHealth,
                        Price = 10
                    });
                }
                else
                {
                    view.DrawUpgradeItem(BuildUpgradeData(unitData));
                }
            }

            playerInteract.EnterInteractState(Panels.PanelType.unitUpgrade);
        }

        private void OnTryToClose()
        {
            if (playerInteract.CheckInteraction(Panels.PanelType.unitUpgrade) == false)
                throw new Exception();

            playerInteract.ExitInteractState();
        }

        private void OnTryHeal(int unitId)
        {
            bool result = upgradeSystem.TryHealUnit(unitId);

            if (result) view.DrawHealSuccess(BuildUpgradeData(gameState.GetUnitDataById(unitId)));
            else view.DrawHealFailed(unitId);
        }

        private void OnTryUpgrade(int unitId)
        {
            bool result = upgradeSystem.TryUpgradeUnit(unitId);

            if (result) view.DrawUpgradeSuccess(BuildUpgradeData(gameState.GetUnitDataById(unitId)));
            else view.DrawUpgradeFailed(unitId);
        }

        private UnitToUpgradeData BuildUpgradeData(UnitData unitData)
        {
            var upgradeData = new UnitToUpgradeData();
            upgradeData.Id = unitData.Id;
            upgradeData.IsMaxLevel = unitData.UpgradeLevel == 3;

            upgradeData.BodyType = unitData.BodyType;

            upgradeData.Health = unitData.MaxHealth;
            upgradeData.Speed = unitData.Speed;
            upgradeData.Damage = unitData.Damage;

            if (upgradeData.IsMaxLevel == false)
            {
                upgradeData.Price = 10 * (unitData.UpgradeLevel + 1);

                switch (unitData.BodyType)
                {
                    case UnitBodyType.small:
                        upgradeData.UpgradeStatsInfo = "xxo";
                        break;
                    case UnitBodyType.medium:
                        upgradeData.UpgradeStatsInfo = "xox";
                        break;
                    case UnitBodyType.big:
                        upgradeData.UpgradeStatsInfo = "oxx";
                        break;
                }
            }

            return upgradeData;
        }
    }
}
