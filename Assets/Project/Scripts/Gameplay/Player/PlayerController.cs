﻿using Gameplay.CameraSystem;
using Gameplay.Game.ECS;
using Gameplay.Game.ECS.Features;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerController : MonoBehaviour, IPlayerController, IEcsEntityHolder
    {
        public Transform Pivot => transform;
        public EcsEntity EcsEntity { get; private set; }
        public IGameCamera GameCamera { get; private set; }

        [field: SerializeField] public CharacterController CharacterController { get; private set; }

        public void Initialize(EcsEntity entity, IGameCamera gameCamera)
        {
            EcsEntity = entity;
            GameCamera = gameCamera;
        }

        public void SetFreeze(bool freeze)
        {
            if (freeze) EcsEntity.Get<BlockFreezed>();
            else EcsEntity.Del<BlockFreezed>();
        }
    }
}
