using UnityEngine;

namespace Gameplay.Game.ECS.Features
{
    public struct DamageZone
    {
        public Vector3 Center;
        public Vector3 Border;
        public Quaternion Orientation;
        public float Damage;
    }
}
