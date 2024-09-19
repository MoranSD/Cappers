using Gameplay.EnemySystem.BaseEnemy;
using Utils.StateMachine;

namespace Gameplay.EnemySystem.Behaviour
{
    public class EnemyState<T> : IState where T : EnemyController 
    {
        protected T enemyController;

        public EnemyState(T enemyController)
        {
            this.enemyController = enemyController;
        }
    }
}
