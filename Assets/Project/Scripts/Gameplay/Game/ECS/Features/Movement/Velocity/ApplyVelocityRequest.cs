﻿using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Game.ECS.Features
{
    public struct ApplyVelocityRequest
    {
        public EcsEntity Target;
        public Vector3 Direction;
        public float Force;
        public bool IsTemporary;
        public float Duration;
    }
}
