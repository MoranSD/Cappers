﻿using Leopotam.Ecs;
using UnityEngine;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class ApplyDamageSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
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

                _world.NewOneFrameEntity(new ApplyDamageEvent()
                {
                    Sender = attackRequest.Sender,
                    Target = attackRequest.Target,
                    Damage = attackRequest.Damage,
                });
            }
        }
    }
}
