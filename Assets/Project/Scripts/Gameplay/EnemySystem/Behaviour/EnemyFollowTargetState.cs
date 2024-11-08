using Gameplay.EnemySystem.BaseEnemy;
using UnityEngine;
using Utils;
using Utils.StateMachine;

namespace Gameplay.EnemySystem.Behaviour
{
    public class EnemyFollowTargetState : EnemyState<EnemyController>, IEnterableState, IUpdateableState, IExitableState
    {
        private IAttackTarget lastTarget;
        private float updateDestinationTime;

        public EnemyFollowTargetState(EnemyController enemyController) : base(enemyController)
        {
        }

        public void Enter()
        {
            lastTarget = null;
            updateDestinationTime = enemyController.Config.FollowConfig.UpdateDestinationRate;
        }

        public void Exit()
        {
            enemyController.View.Movement.Stop();
        }

        public void Update(float deltaTime)
        {
            var ourPosition = enemyController.View.Movement.GetPosition();
            var visionRange = enemyController.Config.LookConfig.VisionRange;

            if (enemyController.View.Look.TryGetTargetAround(visionRange, out var currentTarget) == false)
            {
                if(lastTarget == null)
                {
                    enemyController.StateController.ChangeState<EnemyIdleState>();
                    return;
                }
                else
                {
                    currentTarget = lastTarget;
                }
            }

            var targetPosition = currentTarget.GetPosition();

            if (lastTarget != null && lastTarget != currentTarget)
            {
                if (Vector3.Distance(ourPosition, lastTarget.GetPosition()) < Vector3.Distance(ourPosition, targetPosition))
                    currentTarget = lastTarget;
            }

            if (Vector3.Distance(ourPosition, targetPosition) >= enemyController.Config.FollowConfig.MaxFollowDistance)
            {
                enemyController.StateController.ChangeState<EnemyIdleState>();
                return;
            }
            else if (Vector3.Distance(ourPosition, targetPosition) <= enemyController.Config.AttackConfig.AttackDistance)
            {
                enemyController.StateController.ChangeState<EnemyAttackState, IAttackTarget>(currentTarget);
                return;
            }

            lastTarget = currentTarget;

            updateDestinationTime += deltaTime;
            if(updateDestinationTime >= enemyController.Config.FollowConfig.UpdateDestinationRate)
            {
                updateDestinationTime = 0;
                UpdateDestination();
            }
        }

        private void UpdateDestination()
        {
            var destinationPosition = lastTarget.GetPosition();
            var moveSpeed = enemyController.Config.MovementConfig.Speed;
            enemyController.View.Movement.SetDestination(destinationPosition, moveSpeed);
        }
    }
}
