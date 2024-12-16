using Infrastructure.TickManagement;
using System;

namespace Gameplay.Ship.Fight.Hole
{
    public class ShipHole : ITickable
    {
        public event Action OnFixed;

        public bool IsFixed { get; private set; } = false;

        private readonly IShipHoleView view;
        private readonly ShipFight ship;

        private const float damageRate = 1;
        private const float damage = 5;

        private float damageTime = damageRate;

        public ShipHole(IShipHoleView view, ShipFight ship)
        {
            this.view = view;
            this.ship = ship;
        }
        public void Initialize()
        {
            view.OnFix += OnFix;
        }
        public void Dispose()
        {
            view.OnFix -= OnFix;
        }
        public void Update(float deltaTime)
        {
            if (IsFixed) return;

            damageTime -= deltaTime;
            if (damageTime > 0) return;

            damageTime += damageRate;
            ship.ApplyDamage(damage);
            view.DrawDamage();
        }
        private void OnFix()
        {
            IsFixed = true;
            view.Hide();
            OnFixed?.Invoke();
        }
    }
}
