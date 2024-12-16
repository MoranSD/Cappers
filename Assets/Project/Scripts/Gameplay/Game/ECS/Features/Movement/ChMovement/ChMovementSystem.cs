using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Game.ECS.Features
{
    public class ChMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ChMovableComponent, MoveSpeedData, MoveDirectionData>.Exclude<BlockFreezed> filter = null;
        private readonly EcsFilter<MoveEvent> filter2 = null;

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
            foreach (var i in filter2)
            {
                ref var mEvent = ref filter2.Get1(i);
                ref var target = ref mEvent.Target;

                if (target.Has<ChMovableComponent>())
                {
                    ref var controller = ref target.Get<ChMovableComponent>().CharacterController;

                    var moveDirection = mEvent.Direction * mEvent.Speed * deltaTime;
                    controller.Move(moveDirection);
                }
            }
        }
    }
}
