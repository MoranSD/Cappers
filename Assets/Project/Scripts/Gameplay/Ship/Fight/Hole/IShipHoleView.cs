using System;

namespace Gameplay.Ship.Fight.Hole
{
    public interface IShipHoleView
    {
        event Action OnFix;

        void Hide();
        void DrawDamage();
    }
}
