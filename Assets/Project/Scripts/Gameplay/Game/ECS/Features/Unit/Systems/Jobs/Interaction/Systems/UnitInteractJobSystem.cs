using Leopotam.Ecs;
using UnityEngine;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class UnitInteractJobSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TagUnit, TranslationComponent, UnitInteractJobComponent>.Exclude<BlockUnitInteractJob> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var job = ref filter.Get3(i);

                if(job.Interactable == null || job.Interactable.IsInteractable == false)
                {
                    OnEndJob();
                    continue;
                }

                ref var transform = ref filter.Get2(i).Transform;

                if (Vector3.Distance(transform.position, job.Interactable.Pivot.position) <= GameConstants.UnitInteractDistance)
                {
                    ref var controller = ref filter.Get1(i).Controller;
                    job.Interactable.Interact(controller);
                    OnEndJob();
                }

                void OnEndJob()
                {
                    ref var entity = ref filter.GetEntity(i);
                    entity.Del<UnitInteractJobComponent>();
                    EventBus.Invoke<EndInteractJobEvent>(new()
                    {
                        Entity = entity,
                    });
                }
            }
        }
    }
}
