using UnityEngine;

namespace Gameplay.Game.ECS.Features
{
    public struct ArcMovementComponent
    {
        public Vector3 Start;
        public Vector3 End;

        public AnimationCurve HighCurve;

        public float Progress;
        public float Duration;
    }
}
