using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Game.ECS.Features
{
    public struct AgentSetDestinationRequest
    {
        public EcsEntity Target;
        public Vector3 Destination;
    }
}
