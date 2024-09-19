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
                var closestTarget = GetClosestTarget(targets);
                controller.Movement.SetLookTarget(closestTarget);
            }
            else
            {
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

            meleeDelayTime = controller.Config.FightConfig.MeleeAttackDelay;
        }

        private void OnLongAttack()
        {
            if (HandleInput == false) return;
            if (longDelayTime != 0) return;

            longDelayTime = controller.Config.FightConfig.LongAttackDelay;
        }

        private IAttackTarget GetClosestTarget(IAttackTarget[] targets)
        {
            var closestTarget = targets[0];

            foreach (var target in targets)
            {
                if(Vector3.Distance(controller.View.Movement.GetPosition(), target.GetPosition()) <
                    Vector3.Distance(controller.View.Movement.GetPosition(), closestTarget.GetPosition()))
                    closestTarget = target;
            }

            return closestTarget;
        }
    }
}
