using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Game.ECS.Features
{
    public class WeaponReloadCoolDownSystem : IEcsRunSystem
    {
        private readonly EcsFilter<WeaponCoolDownComponent> filter = null;

        public void Run()
        {
            float deltaTime = Time.deltaTime;
            foreach (var i in filter)
            {
                ref var weapon = ref filter.Get1(i);

                if(weapon.AttackCoolDown > 0)
                {
                    weapon.AttackCoolDown -= deltaTime;
                }
            }
        }
    }
}
