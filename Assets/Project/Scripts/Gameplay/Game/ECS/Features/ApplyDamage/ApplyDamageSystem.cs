using Leopotam.Ecs;
using UnityEngine;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class ApplyDamageSystem : IEcsInitSystem, IEcsDestroySystem
    {
        public void Init()
        {
            EventBus.Subscribe<ApplyDamageRequest>(OnApplyDamage);
        }

        public void Destroy()
        {
            EventBus.Unsubscribe<ApplyDamageRequest>(OnApplyDamage);
        }

        private void OnApplyDamage(ApplyDamageRequest attackRequest)
        {
            if (attackRequest.Target.IsAlive() == false) return;
            if (attackRequest.Target.Has<HealthComponent>() == false)
            {
                Debug.Log("No health on target");
                return;
            }

            ref var targetHealth = ref attackRequest.Target.Get<HealthComponent>();

            targetHealth.Health -= attackRequest.Damage;

            EventBus.Invoke(new ApplyDamageEvent()
            {
                Sender = attackRequest.Sender,
                Target = attackRequest.Target,
                Damage = attackRequest.Damage,
            });
        }
    }
}
