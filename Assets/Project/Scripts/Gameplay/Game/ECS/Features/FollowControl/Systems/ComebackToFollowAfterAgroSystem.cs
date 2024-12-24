using Leopotam.Ecs;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class ComebackToFollowAfterAgroSystem : IEcsInitSystem, IEcsDestroySystem
    {
        public void Destroy()
        {
            EventBus.Unsubscribe<EndAgroEvent>(OnEndAgro);
        }

        public void Init()
        {
            EventBus.Subscribe<EndAgroEvent>(OnEndAgro);
        }

        private void OnEndAgro(EndAgroEvent endsAgroEvent)
        {
            ref var entity = ref endsAgroEvent.Entity;

            if (entity.Has<TagUnderFollowControl>() == false)
                return;

            ref var followOwner = ref entity.Get<TagUnderFollowControl>().Owner;

            EventBus.Invoke(new AddFollowControlRequest()
            {
                Sender = followOwner,
                Target = entity,
            });
        }
    }
}
