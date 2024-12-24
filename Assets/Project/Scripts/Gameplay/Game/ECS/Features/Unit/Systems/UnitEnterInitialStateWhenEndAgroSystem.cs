using Leopotam.Ecs;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class UnitEnterInitialStateWhenEndAgroSystem : IEcsInitSystem, IEcsDestroySystem
    {
        public void Destroy()
        {
            EventBus.Unsubscribe<EndAgroEvent>(OnEndAgro);
        }

        public void Init()
        {
            EventBus.Subscribe<EndAgroEvent>(OnEndAgro, 1);
        }

        private void OnEndAgro(EndAgroEvent beginsAgroEvent)
        {
            ref var entity = ref beginsAgroEvent.Entity;

            if (entity.Has<TagUnit>() == false)
                return;

            entity.Get<TagAvailableForFollowControlInteraction>();

            if (entity.Has<TagUnderFollowControl>())
                return;

            ref var unitTag = ref entity.Get<TagUnit>();
            unitTag.Controller.GoToIdlePosition();
        }
    }
}
