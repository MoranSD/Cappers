using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public class PreventAttackByCoolDownSystem : IEcsRunSystem
    {
        private readonly EcsFilter<AttackRequest> filter = null;
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var request = ref filter.Get1(i);
                ref var attackSender = ref request.Sender;

                if (attackSender.Has<AttackCoolDownComponent>())
                {
                    ref var coolDownComponent = ref attackSender.Get<AttackCoolDownComponent>();

                    if (coolDownComponent.AttackCoolDown <= 0)
                        continue;

                    ref var requestEntity = ref filter.GetEntity(i);
                    requestEntity.Del<AttackRequest>();
                }
            }
        }
    }
}
