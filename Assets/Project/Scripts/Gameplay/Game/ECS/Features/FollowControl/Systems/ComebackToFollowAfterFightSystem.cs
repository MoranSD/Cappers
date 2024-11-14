using Leopotam.Ecs;
using System.Linq;

namespace Gameplay.Game.ECS.Features
{
    public class ComebackToFollowAfterFightSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<TagUnderFollowControl, TargetAgroComponent, TargetLookComponent> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var agro = ref filter.Get2(i);

                /*
                 * if unit now or was in fight, he should have agro.HasTarget as true
                 * than his target should be dead (IsAlive() == false)
                 * 
                 * so, if he has no targets alive he comes back to player (if he is under player control) 
                 * or goes to his idle position
                 */

                if (agro.HasTarget == false) continue;
                if (agro.Target.IsAlive()) continue;
                ref var look = ref filter.Get3(i);
                if (look.Targets.Any(x => x.IsAlive())) continue;

                ref var followOwner = ref filter.Get1(i).Owner;
                ref var entity = ref filter.GetEntity(i);
                var requestEntity = _world.NewEntity();
                ref var addFollowControlRequest = ref requestEntity.Get<AddFollowControlRequest>();

                addFollowControlRequest.Sender = followOwner;
                addFollowControlRequest.Target = entity;
            }
        }
    }
}
