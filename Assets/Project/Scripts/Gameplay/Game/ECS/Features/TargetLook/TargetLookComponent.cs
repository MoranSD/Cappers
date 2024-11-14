using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Game.ECS.Features
{
    public struct TargetLookComponent
    {
        public LayerMask TargetLayer;
        public float Range;
        public bool HasTargetsInRange;
        public EcsEntity[] Targets;
    }
}
