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

            playerEntity.Get<TagPlayer>();

            ref var targetLook = ref playerEntity.Get<TargetLookComponent>();
            targetLook.Range = playerConfig.MainConfig.FightConfig.AttackRange;
            targetLook.TargetLayer = gameConfig.PlayerTargetLayers;

            ref var unitControl = ref playerEntity.Get<FollowControlComponent>();
            unitControl.EntitiesInControl = new();

            ref var translation = ref playerEntity.Get<TranslationComponent>();
            translation.Transform = player.Pivot;

            ref var movable = ref playerEntity.Get<ChMovableComponent>();
            movable.CharacterController = player.CharacterController;
            movable.Speed = playerConfig.MainConfig.MovementConfig.MoveSpeed;

            ref var turnable = ref playerEntity.Get<TFTurnableComponent>();

            //InteractController
            var panelsManager = ServiceLocator.Get<PanelsManager>();
            ServiceLocator.Register(new PlayerMenuInteractController(player, panelsManager));
        }

        public override void Dispose()
        {
            ServiceLocator.Remove<IGameCamera>();

            var interactController = ServiceLocator.Remove<PlayerMenuInteractController>();
            interactController.Dispose();
        }
    }
}
