using Leopotam.Ecs;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class AgroTargetSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<TargetAgroComponent, WeaponLink> filter = null;

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

                _world.NewEntity()
                    .Replace(new AttackRequest()
                    {
                        WeaponSender = weapon,
                    })
                    .Replace(new AttackRequestTargetData()
                    {
                        Target = agroComponent.Target,
                    })
                    .Replace(new OneFrameEntity());
            }
        }
    }
}
