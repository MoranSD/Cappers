using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Game.ECS.Features
{
    public class CCMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ChMovableComponent, MoveSpeedData, MoveDirectionData>.Exclude<BlockFreezed> filter = null;

        public void Run()
        {
            float deltaTime = Time.deltaTime;

            foreach (var i in filter)
            {
                ref var movable = ref filter.Get1(i);
                ref var speed = ref filter.Get2(i).Speed;
                ref var direction = ref filter.Get3(i).Direction;
                ref var controller = ref movable.CharacterController;

                var moveDirection = direction * speed * deltaTime;
                controller.Move(moveDirection);
            }
        }
    }
}
