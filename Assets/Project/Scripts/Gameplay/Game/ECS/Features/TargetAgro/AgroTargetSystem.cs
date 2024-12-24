using Leopotam.Ecs;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class AgroTargetSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TargetAgroComponent, WeaponLink>.Exclude<BlockFreezed> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var agroComponent = ref filter.Get1(i);

                if (agroComponent.HasTarget == false) continue;

                ref var entity = ref filter.GetEntity(i);

                ref var followComponent = ref entity.Get<FollowComponent>();
                followComponent.Target = agroComponent.Target;

                ref var weapon = ref filter.Get2(i).Weapon;

                if (weapon.Has<AttackCoolDownComponent>())
                {
                    ref var coolDown = ref weapon.Get<AttackCoolDownComponent>();

                    if (coolDown.AttackCoolDown > 0) continue;
                }

                EventBus.Invoke(new AttackRequest()
                {
                    Sender = weapon,
                    ExtensionData = new()
                    {
                        { AttackRequest.TARGET_EXTENSION_DATA_KEY, agroComponent.Target }
                    },
                });
            }
        }
    }
}
