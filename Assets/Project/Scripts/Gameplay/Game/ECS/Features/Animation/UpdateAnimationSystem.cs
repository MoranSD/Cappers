using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public class UpdateAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<AnimationComponent> filter = null;
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var anim = ref filter.Get1(i);
                ref var entity = ref filter.GetEntity(i);
                anim.Updater.Update(ref entity);
            }
        }
    }
}
