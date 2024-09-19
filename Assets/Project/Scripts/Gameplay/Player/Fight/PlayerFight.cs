using UnityEngine;
using Utils;

namespace Gameplay.Player.Fight
{
    public class PlayerFight
    {
        public bool HandleInput = true;

        private readonly PlayerController controller;

        private float meleeDelayTime;
        private float longDelayTime;

        private IAttackTarget closestTarget;

        public PlayerFight(PlayerController controller)
        {
            this.controller = controller;
        }

        public void Initialize()
        {
            controller.Input.OnPressMeleeAttackButton += OnMeleeAttack;
            controller.Input.OnPressLongAttackButton += OnLongAttack;
        }

        public void Update(float deltaTime)
        {
            if(meleeDelayTime != 0)
            {
                meleeDelayTime -= deltaTime;

                if(meleeDelayTime < 0)
                    meleeDelayTime = 0;
            }
            if(longDelayTime != 0)
            {
                longDelayTime -= deltaTime;

                if(longDelayTime < 0) 
                    longDelayTime = 0;
            }

            if(controller.View.Look.TryGetTargetsAround(controller.Config.FightConfig.AttackRange, out var targets))
            {
                closestTarget = GetClosestTarget(targets);
                controller.Movement.SetLookTarget(closestTarget);
            }
            else
            {
                closestTarget = null;
                controller.Movement.ResetLookTarget();
            }
        }

        public void Dispose()
        {
            controller.Input.OnPressMeleeAttackButton -= OnMeleeAttack;
            controller.Input.OnPressLongAttackButton -= OnLongAttack;
            controller.Movement.ResetLookTarget();
        }

        private void OnMeleeAttack()
        {
            if (HandleInput == false) return;
            if (meleeDelayTime != 0) return;
            if (closestTarget == null) return;

            var ourPosition = controller.View.Movement.GetPosition();
            var targetPosition = closestTarget.GetPosition();
            var attackDistance = controller.Config.FightConfig.MeleeAttackDistance;

            if (Vector3.Distance(ourPosition, targetPosition) > attackDistance)
                return;

            meleeDelayTime = controller.Config.FightConfig.MeleeAttackDelay;
            closestTarget.ApplyDamage(controller.Config.FightConfig.BaseMeleeDamage);
            controller.View.Fight.PerformMeleeAttack();
        }

        private void OnLongAttack()
        {
            if (HandleInput == false) return;
            if (longDelayTime != 0) return;
            if (closestTarget == null) return;

            var ourPosition = controller.View.Movement.GetPosition();
            var targetPosition = closestTarget.GetPosition();
            var attackDistance = controller.Config.FightConfig.LongAttackDistance;

            if (Vector3.Distance(ourPosition, targetPosition) > attackDistance)
                return;

            longDelayTime = controller.Config.FightConfig.LongAttackDelay;
            closestTarget.ApplyDamage(controller.Config.FightConfig.BaseLongDamage);
            controller.View.Fight.PerformLongAttack();
        }

        private IAttackTarget GetClosestTarget(IAttackTarget[] targets)
        {
            var ourPosition = controller.View.Movement.GetPosition();
            var closestTarget = targets[0];

            foreach (var target in targets)
            {
                if (Vector3.Distance(ourPosition, target.GetPosition()) <
                    Vector3.Distance(ourPosition, closestTarget.GetPosition()))
                    closestTarget = target;
            }

            return closestTarget;
        }
    }
}
