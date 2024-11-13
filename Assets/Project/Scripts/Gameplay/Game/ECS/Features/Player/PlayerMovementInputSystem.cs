using Gameplay.CameraSystem;
using Infrastructure.GameInput;
using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public class PlayerMovementInputSystem : IEcsRunSystem
    {
        private readonly IInput input = null;
        private readonly IGameCamera gameCamera = null;
        private readonly EcsFilter<TagPlayer, ChMovableComponent> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                var moveInput = input.MoveInput;

                var moveDirection = gameCamera.Forward * moveInput.y + gameCamera.Right * moveInput.x;
                moveDirection.y = 0;
                moveDirection.Normalize();

                ref var move = ref filter.Get2(i);
                ref var direction = ref move.Direction;

                direction = moveDirection;
            }
        }
    }
}
