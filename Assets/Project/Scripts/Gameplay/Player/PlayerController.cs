using Gameplay.CameraSystem;
using Gameplay.Game.ECS.Features;
using Gameplay.Player.Data;
using Infrastructure.GameInput;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        public Transform Pivot => transform;
        public EcsEntity EcsEntity { get; private set; }
        public IGameCamera GameCamera { get; private set; }
        public IInput Input { get; private set; }
        public PlayerConfig Config { get; private set; }

        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public LayerMask InteractorLayer { get; private set; }
        [field: SerializeField] public LayerMask EnemyLayer { get; private set; }

        public void Initialize(EcsEntity entity, IGameCamera gameCamera, IInput input, PlayerConfig config)
        {
            EcsEntity = entity;
            GameCamera = gameCamera;
            Config = config;
            Input = input;
        }

        public void SetFreeze(bool freeze)
        {
            if (freeze) EcsEntity.Get<BlockFreezed>();
            else EcsEntity.Del<BlockFreezed>();
        }
    }
}
