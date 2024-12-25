using Leopotam.Ecs;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class PreventAttackByCoolDownSystem : IEcsInitSystem, IEcsDestroySystem
    {
        public void Init()
        {
            EventBus.Subscribe<AttackRequest>(OnWeaponAttack, 1);
        }

        public void Destroy()
        {
            EventBus.Unsubscribe<AttackRequest>(OnWeaponAttack);
        }

        private void OnWeaponAttack(AttackRequest request)
        {
            if (request.IsAbleToAttack == false) return;

            ref var attackSender = ref request.Sender;

            if (attackSender.Has<AttackCoolDownComponent>())
            {
                ref var coolDownComponent = ref attackSender.Get<AttackCoolDownComponent>();
                request.IsAbleToAttack = coolDownComponent.AttackCoolDown <= 0;
            }
        }
    }
}
