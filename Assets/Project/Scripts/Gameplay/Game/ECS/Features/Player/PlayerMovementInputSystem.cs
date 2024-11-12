using Gameplay.Player;
using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public class PlayerMovementInputSystem : IEcsRunSystem
    {
        private readonly PlayerController playerController = null;
        private readonly EcsFilter<TagPlayer, ChMovableComponent> filter = null;

        public void Run()
        {
            if (filter.GetEntitiesCount() == 0) return;

            var moveInput = playerController.Input.MoveInput;

            var moveDirection = playerController.GameCamera.Forward * moveInput.y + playerController.GameCamera.Right * moveInput.x;
            moveDirection.y = 0;
            moveDirection.Normalize();

            foreach (var i in filter)
            {
                ref var move = ref filter.Get2(i);
                ref var direction = ref move.Direction;

                direction = moveDirection;
            }
        }
    }
}
