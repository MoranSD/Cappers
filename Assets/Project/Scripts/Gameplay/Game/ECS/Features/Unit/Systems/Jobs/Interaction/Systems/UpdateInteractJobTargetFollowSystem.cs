using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public class UpdateInteractJobTargetFollowSystem : IEcsRunSystem
    {
        private readonly EcsFilter<FollowComponent, UnitInteractJobComponent>.Exclude<BlockUnitInteractJob> filter = null;
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var follow = ref filter.Get1(i);
                ref var job = ref filter.Get2(i);

                follow.Target = job.Interactable.Pivot;
            }
        }
    }
}
