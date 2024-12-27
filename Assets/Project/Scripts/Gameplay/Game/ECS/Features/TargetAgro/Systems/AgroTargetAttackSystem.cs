using Leopotam.Ecs;
using UnityEngine;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class AgroTargetAttackSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TargetAgroComponent, WeaponLink>.Exclude<BlockAgro> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var agroComponent = ref filter.Get1(i);

                if (agroComponent.HasTarget == false) continue;
                /*
                 * пока только может быть такое, 
                 * что цель убили и дальше по списку пошли целы, 
                 * которые нацелены на ту же цель
                 * 
                 * это так то плохо, это должно быть не сразу, ну похуй
                 * если делать с здаержкой, то может быть проблема,
                 * а пока тупо есть проверка, то ничего не багается
                 */
                if (agroComponent.Target.IsAlive() == false) continue;

                ref var weapon = ref filter.Get2(i).Weapon;

                if (weapon.Has<AttackCoolDownComponent>())
                {
                    ref var coolDown = ref weapon.Get<AttackCoolDownComponent>();

                    if (coolDown.AttackCoolDown > 0) continue;
                }

                if (weapon.Has<WeaponAttackDistanceData>())
                {
                    ref var entity = ref filter.GetEntity(i);
                    ref var distanceData = ref weapon.Get<WeaponAttackDistanceData>();

                    if (EntityUtil.GetDistance(ref entity, ref agroComponent.Target) > distanceData.AttackDistance) continue;
                }

                EventBus.Invoke(new AttackRequest()
                {
                    Sender = weapon,
                    ExtensionData = new()
                    {
                        { AttackRequest.TARGET_EXTENSION_DATA_KEY, agroComponent.Target }
                    },
                });
            }
        }
    }
}
