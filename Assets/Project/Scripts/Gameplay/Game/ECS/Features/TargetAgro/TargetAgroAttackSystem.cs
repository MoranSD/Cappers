using Leopotam.Ecs;
using UnityEngine;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class TargetAgroAttackSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<TranslationComponent, TargetAgroComponent> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var agroComponent = ref filter.Get2(i);

                if (agroComponent.HasTarget == false) continue;

                ref var tranform = ref filter.Get1(i).Transform;

                ref var targetTF = ref agroComponent.Target.Get<TranslationComponent>().Transform;

                float distanceToTarget = Vector3.Distance(tranform.position, targetTF.position);

                if (distanceToTarget > agroComponent.AttackDistance) continue;

                agroComponent.NextAttackTime -= Time.deltaTime;

                if (agroComponent.NextAttackTime <= 0)
                {
                    agroComponent.NextAttackTime = agroComponent.AttackRate;

                    _world.NewEntityWithComponent<ApplyDamageRequest>(new()
                    {
                        Target = agroComponent.Target,
                        Damage = agroComponent.Damage
                    });
                }
            }
        }
    }
}
