using UnityEngine;

namespace Gameplay.Game.ECS.Features
{
    public struct AgentFollowComponent
    {
        public Transform Target;
        public float DestinationUpdateTime;
    }
}
