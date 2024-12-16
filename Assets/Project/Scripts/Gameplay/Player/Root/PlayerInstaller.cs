using Gameplay.CameraSystem;
using Gameplay.Player.Data;
using Gameplay.Player.InteractController;
using Infrastructure;
using Infrastructure.Composition;
using Infrastructure.DataProviding;
using Infrastructure.GameInput;
using Gameplay.Panels;
using UnityEngine;
using Leopotam.Ecs;
using Gameplay.Game.ECS.Features;
using Gameplay.Game;

namespace Gameplay.Player.Root
{
    public class PlayerInstaller : Installer
    {
        [SerializeField] private PlayerController player;
        [Space]
        [SerializeField] private GameCamera gameCamera;

        public override void PreInitialize()
        {
            //Dependencies
            var input = ServiceLocator.Get<IInput>();
            var assetProvider = ServiceLocator.Get<IAssetProvider>();
            var playerConfig = assetProvider.Load<PlayerConfigSO>(Constants.PlayerConfigPath);
            var gameConfig = assetProvider.Load<GameConfig>(Constants.GameConfigPath);

            gameCamera.Initialize();
            ServiceLocator.Register<IGameCamera>(gameCamera);

            //Ecs player creation
            var ecsWorld = ServiceLocator.Get<EcsWorld>();

            var playerEntity = ecsWorld.NewEntity();
            player.Initialize(playerEntity, gameCamera);

            ref var tagPlayer = ref playerEntity.Get<TagPlayer>();
            tagPlayer.Controller = player;

            ref var targetLook = ref playerEntity.Get<TargetLookComponent>();
            targetLook.Range = playerConfig.MainConfig.FightConfig.AttackRange;
            targetLook.TargetLayer = gameConfig.PlayerTargetLayers;

            ref var unitControl = ref playerEntity.Get<FollowControllerComponent>();
            unitControl.EntitiesInControl = new();

            ref var translation = ref playerEntity.Get<TranslationComponent>();
            translation.Transform = player.Pivot;

            ref var movable = ref playerEntity.Get<ChMovableComponent>();
            movable.CharacterController = player.CharacterController;
            ref var gravity = ref playerEntity.Get<ChGravityComponent>();
            gravity.CharacterController = player.CharacterController;
            ref var moveDirection = ref playerEntity.Get<MoveDirectionData>();
            ref var moveSpeed = ref playerEntity.Get<MoveSpeedData>();
            moveSpeed.Speed = playerConfig.MainConfig.MovementConfig.MoveSpeed;

            ref var turnable = ref playerEntity.Get<TFTurnableComponent>();

            CreatePlayerWeapons(playerConfig, ecsWorld, ref playerEntity);

            //InteractController
            var panelsManager = ServiceLocator.Get<PanelsManager>();
            var interactController = ServiceLocator.Register(new PlayerInteractController(player, panelsManager));
            interactController.Initialize();
        }

        private void CreatePlayerWeapons(PlayerConfigSO playerConfig, EcsWorld ecsWorld, ref EcsEntity playerEntity)
        {
            var meleeWeapon = CreateWeaponEntity<MeleeWeaponComponent>(ecsWorld, ref playerEntity,
            new()
            {
                ZonePivot = player.MeleeWeaponDamageZone,
                ZoneBorders = playerConfig.MainConfig.FightConfig.MeleeDamageZoneBorders,
                Damage = playerConfig.MainConfig.FightConfig.BaseMeleeDamage,
            },
            new()
            {
                AttackRate = playerConfig.MainConfig.FightConfig.MeleeAttackDelay,
                AttackCoolDown = 0
            });

            var rangeWeapon = CreateWeaponEntity<DistanceWeaponComponent>(ecsWorld, ref playerEntity,
            new()
            {
                AttackDistance = playerConfig.MainConfig.FightConfig.LongAttackDistance,
                Damage = playerConfig.MainConfig.FightConfig.BaseLongDamage,
            },
            new()
            {
                AttackRate = playerConfig.MainConfig.FightConfig.LongAttackDelay,
                AttackCoolDown = 0
            });

            ref var weaponLink = ref playerEntity.Get<PlayerWeaponLink>();
            weaponLink.MeleeWeapon = meleeWeapon;
            weaponLink.RangeWeapon = rangeWeapon;
        }
        private EcsEntity CreateWeaponEntity<WC>(EcsWorld ecsWorld, ref EcsEntity playerEntity, WC weaponComponent, AttackCoolDownComponent coolDownComponent)
            where WC : struct
        {
            var weapon = ecsWorld.NewEntity();

            weapon.Replace(weaponComponent);
            weapon.Replace(coolDownComponent);

            ref var ownerComponent = ref weapon.Get<WeaponOwnerComponent>();
            ownerComponent.Owner = playerEntity;

            return weapon;
        }

        public override void Dispose()
        {
            ServiceLocator.Remove<IGameCamera>();

            var interactController = ServiceLocator.Remove<PlayerInteractController>();
            interactController.Dispose();
        }
    }
}
