using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Game.ECS.Features
{
    public struct TryFindAndUpdateFollowControlTargetStateRequest
    {
        public EcsEntity Sender;
        public LayerMask TargetLayer;
        public float Range;
    }
}
