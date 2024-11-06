using System;

namespace Gameplay.Components.Health
{
    public class HealthComponent
    {
        public event Action OnDie;
        public float Health { get; private set; }

        private readonly IHealthView view;

        public HealthComponent(IHealthView view, float startHealth)
        {
            this.view = view;
            Health = startHealth;
        }

        public void ApplyDamage(float damage)
        {
            if (Health <= 0) return;

            Health -= damage;

            if (Health > 0)
            {
                view.DrawGetDamage();
            }
            else
            {
                view.DrawDie();
                OnDie?.Invoke();
            }
        }
    }
}
