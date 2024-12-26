using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Game.ECS.Features
{
    public struct FollowControlledComponent
    {
        public EcsEntity Owner;
        public Transform OwnerTransform;
    }
}
