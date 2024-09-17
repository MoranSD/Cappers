using Gameplay.EnemySystem.View;
using Utilities.BehaviourTree;

namespace Gameplay.EnemySystem.Behaviour
{
    public class EnemyStopMovement : BehaviourNode
    {
        private readonly IBaseEnemyView enemyView;

        public EnemyStopMovement(IBaseEnemyView enemyView)
        {
            this.enemyView = enemyView;
        }

        protected override void OnEnter()
        {
            enemyView.Animation.SetAnimation(EnemyConstants.IdleAnimationName);
            enemyView.Movement.Stop();
            Stop(true);
        }
    }
}
