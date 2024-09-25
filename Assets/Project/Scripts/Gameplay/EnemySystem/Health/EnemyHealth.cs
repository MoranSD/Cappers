using Gameplay.EnemySystem.BaseEnemy;
using System;

namespace Gameplay.EnemySystem.Health
{
    public class EnemyHealth
    {
        public event Action OnDie;
        public float Health { get; private set; }

        private readonly EnemyController controller;
        private readonly IEnemyHealthView view;

        public EnemyHealth(EnemyController controller)
        {
            this.controller = controller;
            view = controller.View.Health;
            Health = controller.Config.HealthConfig.StartHealthCount;
        }

        public void Initialize()
        {
            view.OnGetDamage += OnGetDamage;
        }

        public void Dispose()
        {
            view.OnGetDamage -= OnGetDamage;
        }

        private void OnGetDamage(float damage)
        {
            if (Health <= 0) return;

            Health -= damage;
            
            if(Health > 0)
            {
                view.DrawGetDamage();
            }
            else
            {
                controller.StateController.ExitCurrent();
                view.DrawDie();
                OnDie?.Invoke();
            }
        }
    }
}
