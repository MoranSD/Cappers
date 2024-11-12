using Utils;

namespace Gameplay.Player.Movement
{
    public class PlayerMovement
    {
        public float SpeedFactor = 1;

        private readonly OldPlayerController controller;

        private IAttackTarget lookTarget;

        public PlayerMovement(OldPlayerController controller)
        {
            this.controller = controller;
        }

        public void SetLookTarget(IAttackTarget target)
        {
            lookTarget = target;
        }

        public void ResetLookTarget()
        {
            lookTarget = null;
        }

        public void Update(float deltaTime)
        {
            var moveInput = controller.Input.MoveInput;

            var moveDirection = controller.GameCamera.Forward * moveInput.y + controller.GameCamera.Right * moveInput.x;
            moveDirection.y = 0;
            moveDirection.Normalize();

            var moveSpeed = controller.Config.MovementConfig.MoveSpeed * SpeedFactor * deltaTime;

            controller.View.Movement.Move(moveDirection, moveSpeed);
            
            if(lookTarget != null)
            {
                var directionToTarget = lookTarget.GetPosition() - controller.View.Movement.GetPosition();
                controller.View.Movement.Turn(directionToTarget, controller.Config.MovementConfig.TargetLookSpeed * deltaTime);
            }
            else
            {
                controller.View.Movement.Turn(moveDirection, controller.Config.MovementConfig.LookSpeed * deltaTime);
            }
        }
    }
}
