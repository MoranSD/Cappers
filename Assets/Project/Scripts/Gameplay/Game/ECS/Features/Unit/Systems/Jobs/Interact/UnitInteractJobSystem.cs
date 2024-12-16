using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Game.ECS.Features
{
    public class UnitInteractJobSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TagUnit, TranslationComponent, UnitInteractJobComponent> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var controller = ref filter.Get1(i).Controller;
                ref var transform = ref filter.Get2(i).Transform;
                ref var job = ref filter.Get3(i);

                if (Vector3.Distance(transform.position, job.Interactable.Position) <= GameConstants.UnitInteractDistance)
                {
                    job.Interactable.Interact(controller);

                    ref var entity = ref filter.GetEntity(i);
                    entity.Del<UnitInteractJobComponent>();
                }
            }
        }
    }
}
