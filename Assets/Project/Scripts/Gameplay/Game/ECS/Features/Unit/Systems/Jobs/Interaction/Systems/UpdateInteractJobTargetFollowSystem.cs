using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public class UpdateInteractJobTargetFollowSystem : IEcsRunSystem
    {
        private readonly EcsFilter<UnitInteractJobComponent>.Exclude<BlockUnitInteractJob> filter = null;
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var follow = ref filter.GetEntity(i).Get<FollowComponent>();
                ref var job = ref filter.Get1(i);

                follow.Target = job.Interactable.Pivot;
            }
        }
    }
}
