using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Game.ECS.Features
{
    public class ChMovementPhysicsSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ChMovableComponent> filter = null;

        public void Run()
        {
            float deltaTime = Time.deltaTime;
            var gravityDirection = Physics.gravity * deltaTime;

            foreach (var i in filter)
            {
                ref var movable = ref filter.Get1(i);
                ref var controller = ref movable.CharacterController;

                controller.Move(gravityDirection);
            }
        }
    }
}
