using Gameplay.EnemySystem.Behaviour;
using UnityEngine;
using Utils;
using Utils.StateMachine;

namespace Gameplay.EnemySystem.MeleeEnemy
{
    public class MeleeEnemyAttackState : EnemyState<MeleeEnemyController>, IEnemyAttackState, IUpdateableState
    {
        private IAttackTarget target;
        private float nextAttackTime;

        public MeleeEnemyAttackState(MeleeEnemyController enemyController) : base(enemyController)
        {
        }

        public void Enter(IAttackTarget target)
        {
            this.target = target;
            enemyController.View.Animation.SetAnimation(EnemyConstants.IdleAnimationName);

            if (nextAttackTime <= enemyController.Config.AttackConfig.MinTimeToUpdateDelay)
                nextAttackTime = enemyController.Config.AttackConfig.FirstAttackDelay;
        }

        public void Update(float deltaTime)
        {
            var ourPosition = enemyController.View.Movement.GetPosition();
            var targetPosition = target.GetPosition();

            if (Vector3.Distance(ourPosition, targetPosition) > enemyController.Config.AttackConfig.AttackDistance)
            {
                enemyController.StateController.ChangeState<EnemyFollowTargetState, IAttackTarget>(target);
                return;
            }

            nextAttackTime -= deltaTime;
            if(nextAttackTime <= 0)
            {
                PerformAttack();
                nextAttackTime = enemyController.Config.AttackConfig.AttackRate;
            }
        }

        private void PerformAttack()
        {
            enemyController.View.Animation.SetAnimation(EnemyConstants.AttackAnimationName);
        }
    }
}
