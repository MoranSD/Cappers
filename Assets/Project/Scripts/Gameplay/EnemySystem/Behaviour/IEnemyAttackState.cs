using Utils;
using Utils.StateMachine;

namespace Gameplay.EnemySystem.Behaviour
{
    public interface IEnemyAttackState : IState, IPayloadedEnterableState<IAttackTarget>
    {
    }
}
