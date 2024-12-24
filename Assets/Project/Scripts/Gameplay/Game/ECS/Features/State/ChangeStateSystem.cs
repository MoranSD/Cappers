using Leopotam.Ecs;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class ChangeStateSystem : IEcsInitSystem, IEcsDestroySystem
    {
        public void Destroy()
        {
            EventBus.Unsubscribe<ChangeStateRequest>(OnChangeState);
        }

        public void Init()
        {
            EventBus.Subscribe<ChangeStateRequest>(OnChangeState);
        }

        private void OnChangeState(ChangeStateRequest request)
        {
            ref var targetEntity = ref request.Target;

            if (targetEntity.IsAlive() == false)
                return;

            request.ChangeState.Invoke(ref targetEntity);
        }
    }
}
