using Gameplay.EnemySystem.BaseEnemy;
using UnityEngine;
using Utils;
using Utils.StateMachine;

namespace Gameplay.EnemySystem.Behaviour
{
    public class EnemyFollowTargetState : EnemyState<EnemyController>, IPayloadedEnterableState<IAttackTarget>, IUpdateableState, IExitableState
    {
        private IAttackTarget target;
        private float updateDestinationTime;

        public EnemyFollowTargetState(EnemyController enemyController) : base(enemyController)
        {
        }

        public void Enter(IAttackTarget target)
        {
            this.target = target;
            updateDestinationTime = 0;
            UpdateDestination();
        }

        public void Exit()
        {
            enemyController.View.Movement.Stop();
        }

        public void Update(float deltaTime)
        {
            var ourPosition = enemyController.View.Movement.GetPosition();
            var targetPosition = target.GetPosition();

            if (Vector3.Distance(ourPosition, targetPosition) >= enemyController.Config.FollowConfig.MaxFollowDistance)
            {
                enemyController.StateController.ChangeState<EnemyIdleState>();
                return;
            }
            else if (Vector3.Distance(ourPosition, targetPosition) <= enemyController.Config.AttackConfig.AttackDistance)
            {
                enemyController.StateController.ChangeState<EnemyAttackState, IAttackTarget>(target);
                return;
            }

            updateDestinationTime += deltaTime;
            if(updateDestinationTime >= enemyController.Config.FollowConfig.UpdateDestinationRate)
            {
                updateDestinationTime = 0;
                UpdateDestination();
            }
        }

        private void UpdateDestination()
        {
            var destinationPosition = target.GetPosition();
            var moveSpeed = enemyController.Config.MovementConfig.Speed;
            enemyController.View.Movement.SetDestination(destinationPosition, moveSpeed);
        }
    }
}
