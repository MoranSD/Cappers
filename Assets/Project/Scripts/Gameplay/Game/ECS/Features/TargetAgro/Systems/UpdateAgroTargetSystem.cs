using Leopotam.Ecs;
using System;
using UnityEngine;
using Utils;
using static UnityEngine.EventSystems.EventTrigger;

namespace Gameplay.Game.ECS.Features
{
    public class UpdateAgroTargetSystem : IEcsRunSystem, IEcsInitSystem, IEcsDestroySystem
    {
        private readonly EcsFilter<TranslationComponent, TargetAgroComponent, TargetLookComponent>.Exclude<BlockAgro> filter = null;

        public void Destroy()
        {
            EventBus.Unsubscribe<BeginAgroRequest>(OnBeginAgro);
        }

        public void Init()
        {
            EventBus.Subscribe<BeginAgroRequest>(OnBeginAgro);
        }

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var agroComponent = ref filter.Get2(i);
                ref var lookComponent = ref filter.Get3(i);

                if (lookComponent.HasTargetsInRange)
                {
                    ref var transform = ref filter.Get1(i).Transform;
                    agroComponent.HasTarget = true;
                    agroComponent.Target = EntityUtil.GetClosestEntity(ref transform, ref lookComponent.Targets);
                }
                else if (agroComponent.HasTarget && agroComponent.Target.IsAlive() == false)
                {
                    agroComponent.HasTarget = false;
                    agroComponent.Target = default;
                }

                ref var entity = ref filter.GetEntity(i);
                bool hasTag = entity.Has<TagUnderAgro>();

                if (hasTag == false && agroComponent.HasTarget)
                {
                    ref var targetTF = ref agroComponent.Target.Get<TranslationComponent>().Transform;

                    ref var follow = ref entity.Get<FollowComponent>();
                    follow.Target = targetTF;

                    entity.Get<TagUnderAgro>();

                    EventBus.Invoke(new BeginAgroEvent()
                    {
                        Entity = entity
                    });
                }
                
                if (hasTag && agroComponent.HasTarget == false)
                {
                    entity.Del<FollowComponent>();
                    entity.Del<TagUnderAgro>();

                    EventBus.Invoke(new EndAgroEvent()
                    {
                        Entity = entity
                    });
                }
            }
        }

        private void OnBeginAgro(BeginAgroRequest request)
        {
            if (request.Entity.Has<BlockAgro>())
            {
                Debug.Log("cant add agro to uncontrollable");
                return;
            }

            ref var agroComponent = ref request.Entity.Get<TargetAgroComponent>();

            agroComponent.Target = request.Target;
            agroComponent.HasTarget = true;

            ref var targetTF = ref agroComponent.Target.Get<TranslationComponent>().Transform;

            ref var follow = ref request.Entity.Get<FollowComponent>();
            follow.Target = targetTF;

            if (request.Entity.Has<TagUnderAgro>())
                return;

            request.Entity.Get<TagUnderAgro>();

            EventBus.Invoke(new BeginAgroEvent()
            {
                Entity = request.Entity
            });
        }
    }
}
