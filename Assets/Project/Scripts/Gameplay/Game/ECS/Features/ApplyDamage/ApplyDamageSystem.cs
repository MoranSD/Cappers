using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Game.ECS.Features
{
    public class ApplyDamageSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ApplyDamageRequest> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var attackRequest = ref filter.Get1(i);

                if(attackRequest.Target.Has<HealthComponent>() == false)
                {
                    Debug.Log("No health on target");
                    continue;
                }

                ref var targetHealth = ref attackRequest.Target.Get<HealthComponent>();
                
                targetHealth.Health -= attackRequest.Damage;
            }
        }
    }
}
