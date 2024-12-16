using UnityEngine;
using Leopotam.Ecs;
using Gameplay.Game.ECS.Features;
using System.Linq;

namespace Utils
{
    public static class EntityUtil
    {
        public static Vector3 GetDirectionToEntity(Transform transform, EcsEntity ecsEntity)
        {
            if (ecsEntity.Has<TranslationComponent>() == false)
            {
                Debug.Log($"No TranslationComponent {ecsEntity.GetInternalId()}");
                return Vector3.zero;
            }

            ref var targetTF = ref ecsEntity.Get<TranslationComponent>().Transform;
            return targetTF.position - transform.position;
        }
        public static EcsEntity GetClosestEntity(Transform transform, EcsEntity[] entities)
        {
            EcsEntity closestEntity = entities.First(x => x.Has<TranslationComponent>());
            Vector3 closestEntityPosition = closestEntity.Get<TranslationComponent>().Transform.position;

            foreach (var entity in entities)
            {
                if(entity.Has<TranslationComponent>() == false) continue;

                ref var entityTF = ref entity.Get<TranslationComponent>().Transform;

                if(Vector3.Distance(transform.position, closestEntityPosition) > Vector3.Distance(transform.position, entityTF.position))
                {
                    closestEntity = entity;
                    closestEntityPosition = entityTF.position;
                }
            }

            return closestEntity;
        }
        public static float GetDistance(EcsEntity from, EcsEntity to)
        {
            if (from.Has<TranslationComponent>() == false)
            {
                Debug.Log($"No TranslationComponent 'from' {from.GetInternalId()}");
                return 0f;
            }
            else if (to.Has<TranslationComponent>() == false)
            {
                Debug.Log($"No TranslationComponent 'to' {to.GetInternalId()}");
                return 0f;
            }
            else
            {
                ref var ourTF = ref from.Get<TranslationComponent>().Transform;
                ref var targetTF = ref to.Get<TranslationComponent>().Transform;
                return Vector3.Distance(ourTF.position, targetTF.position);
            }
        }
        public static float GetDistance(Transform transform, EcsEntity entity)
        {
            if (entity.Has<TranslationComponent>() == false)
            {
                Debug.Log($"No TranslationComponent {entity.GetInternalId()}");
                return 0f;
            }
            else
            {
                ref var targetTF = ref entity.Get<TranslationComponent>().Transform;
                return Vector3.Distance(transform.position, targetTF.position);
            }
        }
    }
}
