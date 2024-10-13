using System;

namespace Gameplay.UnitSystem.Buy.Menu.View
{
    public interface IUnitBuyMenuView
    {
        event Action OnTryToClose;
        event Action OnPlayerInteract;
    }
}
