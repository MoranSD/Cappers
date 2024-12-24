using Leopotam.Ecs;
using UnityEngine;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class ChMovementSystem : IEcsRunSystem, IEcsInitSystem, IEcsDestroySystem
    {
        private readonly EcsFilter<ChMovableComponent, MoveSpeedData, MoveDirectionData>.Exclude<BlockFreezed> filter = null;

        public void Destroy()
        {
            EventBus.Unsubscribe<MoveRequest>(OnMove);
        }

        public void Init()
        {
            EventBus.Subscribe<MoveRequest>(OnMove);
        }

        private void OnMove(MoveRequest mRequest)
        {
            ref var target = ref mRequest.Target;

            if (target.Has<ChMovableComponent>())
            {
                ref var controller = ref target.Get<ChMovableComponent>().CharacterController;

                var moveDirection = mRequest.Direction * mRequest.Speed * Time.deltaTime;
                controller.Move(moveDirection);
            }
        }

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
