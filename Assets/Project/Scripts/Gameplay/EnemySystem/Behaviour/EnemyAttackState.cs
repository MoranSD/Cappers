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

            if (Vector3.Distance(ourPosition, targetPosition) > enemyController.Config.AttackConfig.AttackDistance)
            {
                enemyController.StateController.ChangeState<EnemyFollowTargetState, IAttackTarget>(target);
                return;
            }

            nextAttackTime -= deltaTime;
            if(nextAttackTime <= 0)
            {
                enemyController.View.Fight.DrawAttack();
                //TODO: perform attack here
                nextAttackTime = enemyController.Config.AttackConfig.AttackRate;
            }
        }
    }
}
