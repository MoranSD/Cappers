using UnityEngine;

namespace Gameplay.Game.ECS.Features
{
    public struct InteractionEvent
    {
        public Transform Pivot;
        public LayerMask InteractorLayer;
        public float Range;
    }
}
