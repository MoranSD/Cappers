using UnityEngine;
using Leopotam.Ecs;
using Gameplay.Game.ECS.Features;
using System.Linq;

namespace Utils
{
    public static class EntityUtil
    {
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
        public static float GetDistance(Transform transform, EcsEntity entity)
        {
            if(entity.Has<TranslationComponent>() == false)
            {
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
