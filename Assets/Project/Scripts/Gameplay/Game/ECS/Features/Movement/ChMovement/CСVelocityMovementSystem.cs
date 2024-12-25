using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Game.ECS.Features
{
    public class CСVelocityMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ChMovableComponent, VelocityComponent>.Exclude<BlockFreezed> filter = null;
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var movable = ref filter.Get1(i);
                ref var velocity = ref filter.Get2(i);
                ref var controller = ref movable.CharacterController;

                var moveDirection = velocity.Direction * velocity.Force * Time.deltaTime;
                controller.Move(moveDirection);
            }
        }
    }
}
