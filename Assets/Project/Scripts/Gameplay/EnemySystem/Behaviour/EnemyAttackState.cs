using Gameplay.EnemySystem.BaseEnemy;
using UnityEngine;
using Utils;
using Utils.StateMachine;

namespace Gameplay.EnemySystem.Behaviour
{
    public class EnemyAttackState : EnemyState<EnemyController>, IPayloadedEnterableState<IAttackTarget>, IUpdateableState
    {
        private IAttackTarget target;
        private float nextAttackTime;

        public EnemyAttackState(EnemyController enemyController) : base(enemyController)
        {
        }

        public void Enter(IAttackTarget target)
        {
            this.target = target;

            if (nextAttackTime <= enemyController.Config.AttackConfig.MinTimeToUpdateDelay)
                nextAttackTime = enemyController.Config.AttackConfig.FirstAttackDelay;
        }

        public void Update(float deltaTime)
        {
            var ourPosition = enemyController.View.Movement.GetPosition();
            var targetPosition = target.GetPosition();

            if (target.IsDead || Vector3.Distance(ourPosition, targetPosition) > enemyController.Config.AttackConfig.AttackDistance)
            {
                enemyController.StateController.ChangeState<EnemyIdleState>();
                return;
            }

            nextAttackTime -= deltaTime;
            if(nextAttackTime <= 0)
            {
                enemyController.View.Fight.DrawAttack();
                var damage = enemyController.Config.AttackConfig.Damage;
                target.ApplyDamage(damage);
                nextAttackTime = enemyController.Config.AttackConfig.AttackRate;
            }
        }
    }
}
