using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public class WeaponPreventAttackByCoolDownSystem : IEcsRunSystem
    {
        private readonly EcsFilter<WeaponAttackRequest> filter = null;
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var request = ref filter.Get1(i);

                if (request.WeaponSender.Has<WeaponCoolDownComponent>())
                {
                    ref var coolDownComponent = ref request.WeaponSender.Get<WeaponCoolDownComponent>();

                    if (coolDownComponent.AttackCoolDown <= 0)
                        continue;

                    ref var requestEntity = ref filter.GetEntity(i);
                    requestEntity.Del<WeaponAttackRequest>();
                }
            }
        }
    }
}
