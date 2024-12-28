using Leopotam.Ecs;
using UnityEngine;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class UnitInteractJobSystem : IEcsRunSystem, IEcsInitSystem, IEcsDestroySystem
    {
        private readonly EcsFilter<TagUnit, TranslationComponent, UnitInteractJobComponent>.Exclude<BlockUnitInteractJob> filter = null;

        public void Destroy()
        {
            EventBus.Unsubscribe<UnitInteractJobRequest>(OnJob);
            EventBus.Unsubscribe<UnitCancelInteractJobRequest>(OnCancelJob);
        }

        public void Init()
        {
            EventBus.Subscribe<UnitInteractJobRequest>(OnJob);
            EventBus.Subscribe<UnitCancelInteractJobRequest>(OnCancelJob);
        }

        private void OnJob(UnitInteractJobRequest request)
        {
            if (request.Target.Has<TagUnit>() == false) return;
            if (request.Target.Has<BlockUnitInteractJob>())
            {
                Debug.Log("Cant add job to uncontrollable target");
                return;
            }

            ref var follow = ref request.Target.Get<FollowComponent>();
            follow.Target = request.Interactable.Pivot;

            ref var job = ref request.Target.Get<UnitInteractJobComponent>();
            job.Interactable = request.Interactable;
        }

        private void OnCancelJob(UnitCancelInteractJobRequest request)
        {
            if (request.Target.Has<TagUnit>() == false) return;
            if (request.Target.Has<BlockUnitInteractJob>())
            {
                Debug.Log("Cant remove job on uncontrollable target");
                return;
            }

            request.Target.Del<UnitInteractJobComponent>();
            request.Target.Del<FollowComponent>();
            EventBus.Invoke<UnitCanceledInteractJobEvent>(new()
            {
                UnitId = request.Target.Get<TagUnit>().Controller.Id,
            });
        }

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var job = ref filter.Get3(i);

                if (job.Interactable.Equals(null) || job.Interactable.IsInteractable == false)
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
                    entity.Del<FollowComponent>();
                    EventBus.Invoke<UnitEndInteractJobEvent>(new()
                    {
                        Entity = entity,
                    });
                }
            }
        }
    }
}
