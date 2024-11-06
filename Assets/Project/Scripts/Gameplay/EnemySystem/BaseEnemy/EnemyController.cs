using Gameplay.Components.Health;
using Gameplay.EnemySystem.Behaviour;
using Gameplay.EnemySystem.Data;
using Gameplay.EnemySystem.View;
using Infrastructure.TickManagement;
using UnityEngine;
using Utils;
using Utils.StateMachine;

namespace Gameplay.EnemySystem.BaseEnemy
{
    public class EnemyController : ITickable, IAttackTarget
    {
        public readonly EnemyConfig Config;
        public readonly HealthComponent Health;
        public readonly StateController StateController;
        public readonly IEnemyView View;

        public EnemyController(IEnemyView view, EnemyConfig config)
        {
            View = view;
            Config = config;
            Health = new(view.Health, config.HealthConfig.StartHealthCount);
            StateController = new StateController(
                new EnemyIdleState(this),
                new EnemyFollowTargetState(this),
                new EnemyAttackState(this)
                );
        }

        public void Initialize()
        {
            Health.OnDie += OnDie;
            StateController.ChangeState<EnemyIdleState>();
        }

        public void Update(float deltaTime)
        {
            StateController.UpdateCurrentState(deltaTime);
        }

        public void Dispose()
        {
            StateController.DisposeCurrent();
            Health.OnDie -= OnDie;
        }

        private void OnDie()
        {
            StateController.ExitCurrent();
        }

        public Vector3 GetPosition() => View.Movement.GetPosition();

        public void ApplyDamage(float damage) => Health.ApplyDamage(damage);
    }
}
