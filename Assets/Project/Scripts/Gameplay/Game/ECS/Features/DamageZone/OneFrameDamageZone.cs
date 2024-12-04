using System;
using UnityEngine;

namespace Gameplay.Game.ECS.Features
{
    public struct OneFrameDamageZone
    {
        public Vector3 Center;
        public Vector3 Border;
        public Quaternion Orientation;
        public float Damage;
    }
}
