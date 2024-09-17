using Gameplay.EnemySystem.Animation;
using Utilities.BehaviourTree;

namespace Gameplay.EnemySystem.Behaviour
{
    public class EnemyPlayAnimation : BehaviourNode
    {
        private readonly string animationName;
        private readonly IEnemyAnimation enemyAnimation;

        public EnemyPlayAnimation(string animationName, IEnemyAnimation enemyAnimation)
        {
            this.animationName = animationName;
            this.enemyAnimation = enemyAnimation;
        }

        protected override void OnEnter()
        {
            enemyAnimation.SetAnimation(animationName);
            Stop(true);
        }
    }
}
