using Gameplay.UnitSystem.Controller;
using System;

namespace Gameplay.Ship.Fight.Hole
{
    public interface IShipHoleView
    {
        public event Action OnInteracted;
        public event Action<IUnitController> OnUnitInteracted;

        void Hide();
        void DrawDamage();
    }
}
