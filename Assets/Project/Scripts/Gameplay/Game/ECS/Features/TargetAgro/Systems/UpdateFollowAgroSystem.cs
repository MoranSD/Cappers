using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public class UpdateFollowAgroSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TagUnderAgro, TargetAgroComponent>.Exclude<BlockAgro> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var follow = ref filter.GetEntity(i).Get<FollowComponent>();
                ref var agroComponent = ref filter.Get2(i);

                ref var targetTF = ref agroComponent.Target.Get<TranslationComponent>().Transform;
                follow.Target = targetTF;
            }
        }
    }
}
