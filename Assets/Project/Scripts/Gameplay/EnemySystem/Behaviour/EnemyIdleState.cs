using Gameplay.EnemySystem.BaseEnemy;
using Utils;
using Utils.StateMachine;

namespace Gameplay.EnemySystem.Behaviour
{
    public class EnemyIdleState : EnemyState<EnemyController>, IEnterableState, IUpdateableState
    {
        public EnemyIdleState(EnemyController enemyController) : base(enemyController)
        {
        }

        public void Enter()
        {
            var destinationPosition = enemyController.View.GetIdlePosition();
            var moveSpeed = enemyController.Config.MovementConfig.Speed;
            enemyController.View.Movement.SetDestination(destinationPosition, moveSpeed);
        }

        public void Update(float deltaTime)
        {
            var visionRange = enemyController.Config.LookConfig.VisionRange;
            if(enemyController.View.Look.TryGetTargetAround(visionRange, out var attackTarget))
            {
                enemyController.StateController.ChangeState<EnemyFollowTargetState>();
            }
        }
    }
}
