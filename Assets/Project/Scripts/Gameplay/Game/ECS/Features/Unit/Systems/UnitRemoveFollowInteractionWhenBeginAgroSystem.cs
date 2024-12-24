using Leopotam.Ecs;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class UnitRemoveFollowInteractionWhenBeginAgroSystem : IEcsInitSystem, IEcsDestroySystem
    {
        public void Destroy()
        {
            EventBus.Unsubscribe<BeginAgroEvent>(OnBeginAgro);
        }

        public void Init()
        {
            EventBus.Subscribe<BeginAgroEvent>(OnBeginAgro);
        }

        private void OnBeginAgro(BeginAgroEvent beginsAgroEvent)
        {
            ref var entity = ref beginsAgroEvent.Entity;

            if (entity.Has<TagUnit>() == false)
                return;

            entity.Del<TagAvailableForFollowControlInteraction>();
        }
    }
}
