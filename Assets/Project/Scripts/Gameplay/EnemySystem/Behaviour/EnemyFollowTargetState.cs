using Gameplay.EnemySystem.BaseEnemy;
using UnityEngine;
using Utils;
using Utils.StateMachine;

namespace Gameplay.EnemySystem.Behaviour
{
    public class EnemyFollowTargetState : EnemyState<EnemyController>, IEnterableState, IUpdateableState, IExitableState
    {
        private float updateDestinationTime;

        public EnemyFollowTargetState(EnemyController enemyController) : base(enemyController)
        {
        }

        public void Enter()
        {
            updateDestinationTime = enemyController.Config.FollowConfig.UpdateDestinationRate;
        }

        public void Exit()
        {
            enemyController.View.Movement.Stop();
        }

        public void Update(float deltaTime)
        {
            var followVisionRange = enemyController.Config.FollowConfig.MaxFollowDistance;

            if (enemyController.View.Look.TryGetTargetAround(followVisionRange, out var currentTarget) == false)
            {
                enemyController.StateController.ChangeState<EnemyIdleState>();
                return;
            }

            var ourPosition = enemyController.View.Movement.GetPosition();
            var targetPosition = currentTarget.GetPosition();
            var distanceToTarget = Vector3.Distance(ourPosition, targetPosition);

            if (distanceToTarget <= enemyController.Config.AttackConfig.AttackDistance)
            {
                enemyController.StateController.ChangeState<EnemyAttackState, IAttackTarget>(currentTarget);
                return;
            }

            updateDestinationTime += deltaTime;
            if(updateDestinationTime >= enemyController.Config.FollowConfig.UpdateDestinationRate)
            {
                updateDestinationTime = 0;
                UpdateDestination(currentTarget.GetPosition());
            }
        }

        private void UpdateDestination(Vector3 destinationPosition)
        {
            var moveSpeed = enemyController.Config.MovementConfig.Speed;
            enemyController.View.Movement.SetDestination(destinationPosition, moveSpeed);
        }
    }
}
