using System;

namespace Gameplay.UnitSystem.Upgrade.Menu
{
    public interface IUnitUpgradeMenuView
    {
        event Action OnTryToClose;
        event Action OnPlayerInteract;
        event Action<int> OnHeal;
        event Action<int> OnUpgrade;

        void DrawHealItem(UnitToHealData data);
        void DrawHealFailed(int id);
        void DrawHealSuccess(UnitToUpgradeData data);
        void DrawUpgradeItem(UnitToUpgradeData data);
        void DrawUpgradeFailed(int id);
        void DrawUpgradeSuccess(UnitToUpgradeData data);
        void ClearItems();
    }
}
