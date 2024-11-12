using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Game.ECS.Features
{
    public class ChMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ChMovableComponent>.Exclude<BlockFreezed> filter = null;

        public void Run()
        {
            float deltaTime = Time.deltaTime;

            foreach (var i in filter)
            {
                ref var movable = ref filter.Get1(i);
                ref var controller = ref movable.CharacterController;

                var moveDirection = movable.Direction * movable.Speed * deltaTime;
                controller.Move(moveDirection);
            }
        }
    }
}
