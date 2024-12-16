using Gameplay.CameraSystem;
using Gameplay.Game;
using Gameplay.Game.ECS;
using Gameplay.Game.ECS.Features;
using Gameplay.Player.Data;
using Infrastructure;
using Infrastructure.DataProviding;
using Leopotam.Ecs;
using UnityEngine;
using Utils;

namespace Gameplay.Player
{
    public class PlayerController : MonoBehaviour, IPlayerController, IEcsEntityHolder
    {
        public Transform Pivot => transform;
        public EcsEntity EcsEntity { get; private set; }
        public IGameCamera GameCamera { get; private set; }

        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public Transform MeleeWeaponDamageZone { get; private set; }

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

        private void Update()
        {
            if (Input.GetKey(KeyCode.C))
            {
                if (Input.GetKeyDown(KeyCode.M))
                {
                    var world = ServiceLocator.Get<EcsWorld>();
                    var assetProvider = ServiceLocator.Get<IAssetProvider>();
                    var playerConfig = assetProvider.Load<PlayerConfigSO>(Constants.PlayerConfigPath);
                    float slowedSpeed = playerConfig.MainConfig.FightConfig.SlowedMoveSpeed;
                    float slowDownDuration = playerConfig.MainConfig.FightConfig.MeleeMoveSlowDownDuration;
                    world.NewOneFrameEntity(new ApplySlowDownEvent()
                    {
                        Target = EcsEntity,
                        Duration = slowDownDuration,
                        SlowSpeed = slowedSpeed,
                        WithSmoothRecovery = true,
                    });
                }
                else if (Input.GetKeyDown(KeyCode.R))
                {
                    var world = ServiceLocator.Get<EcsWorld>();
                    var assetProvider = ServiceLocator.Get<IAssetProvider>();
                    var playerConfig = assetProvider.Load<PlayerConfigSO>(Constants.PlayerConfigPath);
                    float slowedSpeed = playerConfig.MainConfig.FightConfig.SlowedMoveSpeed;
                    float slowDownDuration = playerConfig.MainConfig.FightConfig.LongMoveSlowDownDuration;
                    world.NewOneFrameEntity(new ApplySlowDownEvent()
                    {
                        Target = EcsEntity,
                        Duration = slowDownDuration,
                        SlowSpeed = slowedSpeed,
                        WithSmoothRecovery = true,
                    });
                }
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                var world = ServiceLocator.Get<EcsWorld>();
                var assetProvider = ServiceLocator.Get<IAssetProvider>();
                var playerConfig = assetProvider.Load<PlayerConfigSO>(Constants.PlayerConfigPath);

                world.NewOneFrameEntity(new ApplyVelocityEvent()
                {
                    Target = EcsEntity,
                    Direction = transform.forward,
                    Force = playerConfig.MainConfig.FightConfig.MeleePushForce,
                    IsTemporary = true,
                    Duration = playerConfig.MainConfig.FightConfig.PushForceDuration,
                });
            }
        }
    }
}
