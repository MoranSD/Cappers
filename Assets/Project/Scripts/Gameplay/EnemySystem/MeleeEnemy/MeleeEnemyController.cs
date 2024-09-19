using Gameplay.EnemySystem.BaseEnemy;
using Gameplay.EnemySystem.Behaviour;
using Gameplay.EnemySystem.Data;
using Gameplay.EnemySystem.View;

namespace Gameplay.EnemySystem.MeleeEnemy
{
    public class MeleeEnemyController : EnemyController
    {
        public MeleeEnemyController(IEnemyView view, EnemyConfig config) : base(view, config)
        {
            StateController.AddState<IEnemyAttackState>(new MeleeEnemyAttackState(this));
        }
    }
}
