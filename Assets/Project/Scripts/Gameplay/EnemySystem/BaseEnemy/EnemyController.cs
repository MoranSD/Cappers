using Gameplay.EnemySystem.Behaviour;
using Gameplay.EnemySystem.Data;
using Gameplay.EnemySystem.View;
using Infrastructure.TickManagement;
using Utils.StateMachine;

namespace Gameplay.EnemySystem.BaseEnemy
{
    public class EnemyController : ITickable
    {
        public readonly EnemyConfig Config;
        public readonly StateController StateController;
        public readonly IEnemyView View;

        public EnemyController(IEnemyView view, EnemyConfig config)
        {
            View = view;
            Config = config;
            StateController = new StateController(
                new EnemyIdleState(this),
                new EnemyFollowTargetState(this)
                );
        }

        public void Initialize()
        {
            StateController.ChangeState<EnemyIdleState>();
        }

        public void Update(float deltaTime)
        {
            StateController.UpdateCurrentState(deltaTime);
        }

        public void Dispose()
        {
            StateController.ExitCurrent();
        }
    }
}
