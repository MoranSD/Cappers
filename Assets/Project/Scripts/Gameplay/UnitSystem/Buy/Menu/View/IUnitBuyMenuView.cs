using Gameplay.UnitSystem.Buy.Data;
using System;

namespace Gameplay.UnitSystem.Buy.Menu.View
{
    public interface IUnitBuyMenuView
    {
        event Action OnTryToClose;
        event Action OnPlayerInteract;
        event Action<int> OnSelectUnitToBuy;

        void DrawBuyItems(UnitToBuyData[] dtos);
        void DrawSoldResult(int unitId, bool success);
    }
}
