using Gameplay.UnitSystem.Controller;
using Infrastructure.TickManagement;
using System;

namespace Gameplay.Ship.Fight.Hole
{
    public class ShipHole : ITickable
    {
        public event Action OnFixed;

        public bool IsFixed { get; private set; } = false;

        public readonly IShipHoleView View;

        private readonly ShipFight ship;

        private const float damageRate = 1;
        private const float damage = 0;

        private float damageTime = damageRate;

        public ShipHole(IShipHoleView view, ShipFight ship)
        {
            this.View = view;
            this.ship = ship;
        }
        public void Initialize()
        {
            View.OnInteracted += OnFix;
            View.OnUnitInteracted += OnFix;
        }
        public void Dispose()
        {
            View.OnInteracted -= OnFix;
            View.OnUnitInteracted -= OnFix;
        }
        public void Update(float deltaTime)
        {
            if (IsFixed) return;

            damageTime -= deltaTime;
            if (damageTime > 0) return;

            damageTime += damageRate;
            ship.ApplyDamage(damage);
            View.DrawDamage();
        }
        private void OnFix()
        {
            IsFixed = true;
            View.Hide();
            OnFixed?.Invoke();
        }
        private void OnFix(IUnitController unit)
        {
            IsFixed = true;
            View.Hide();
            OnFixed?.Invoke();
            unit.GoToIdlePosition();
        }
    }
}
