using Gameplay.EnemySystem.View;
using Infrastructure.TickManagement;
using Utilities.BehaviourTree;

namespace Gameplay.EnemySystem.BaseEnemy
{
    public abstract class BaseEnemyController : ITickable
    {
        protected readonly IBaseEnemyView view;
        protected readonly BehaviourTreeBlackBoard blackboard;
        protected readonly BehaviourTreeRoot behaviourTreeRoot;

        public BaseEnemyController(IBaseEnemyView view)
        {
            this.view = view;

            blackboard = new();
            blackboard.SetValue(EnemyConstants.ViewBlackBoardKey, view);
            blackboard.SetValue(EnemyConstants.ControllerBlackBoardKey, this);

            behaviourTreeRoot = new(CreateIdleBehaviour(), blackboard);
        }

        public void Initialize()
        {
            behaviourTreeRoot.Initialize();
        }

        public void Update(float deltaTime)
        {
            behaviourTreeRoot.Run(deltaTime);
        }

        public void Dispose()
        {
            behaviourTreeRoot.Dispose();
        }

        public void BeginAttackTarget()//IAttackTarget
        {
            var attackBehaviour = CreateAttackBehaviour();
            behaviourTreeRoot.ChangeBehaviour(attackBehaviour);
        }

        protected abstract BehaviourNode CreateIdleBehaviour();
        protected abstract BehaviourNode CreateAttackBehaviour();
    }
}
