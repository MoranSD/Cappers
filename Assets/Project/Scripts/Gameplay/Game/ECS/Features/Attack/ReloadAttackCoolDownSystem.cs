using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Game.ECS.Features
{
    public class ReloadAttackCoolDownSystem : IEcsRunSystem
    {
        private readonly EcsFilter<AttackCoolDownComponent> filter = null;

        public void Run()
        {
            float deltaTime = Time.deltaTime;
            foreach (var i in filter)
            {
                ref var component = ref filter.Get1(i);

                if(component.AttackCoolDown > 0)
                {
                    component.AttackCoolDown -= deltaTime;
                }
            }
        }
    }
}
