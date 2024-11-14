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

namespace Gameplay.Player.Root
{
    public class PlayerInstaller : Installer
    {
        [SerializeField] private PlayerController player;
        [Space]
        [SerializeField] private GameCamera gameCamera;

        public override void PostInitialize()
        {
            //Dependencies
            var input = ServiceLocator.Get<IInput>();
            var assetProvider = ServiceLocator.Get<IAssetProvider>();
            var playerConfig = assetProvider.Load<PlayerConfigSO>(Constants.PlayerConfigPath);

            gameCamera.Initialize();

            //Ecs player creation and systems initialization
            var ecsSystems = ServiceLocator.Get<EcsSystems>();

            ecsSystems
                .Add(new PlayerMovementInputSystem())
                .Add(new PlayerInteractionSystem())
                .Add(new PlayerTurnSystem());

            ecsSystems.Inject(input);
            ecsSystems.Inject(gameCamera, typeof(IGameCamera));
            ecsSystems.Inject(playerConfig);

            var ecsWorld = ServiceLocator.Get<EcsWorld>();

            var playerEntity = ecsWorld.NewEntity();
            player.Initialize(playerEntity, gameCamera);

            playerEntity.Get<TagPlayer>();

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
            var interactController = ServiceLocator.Remove<PlayerMenuInteractController>();
            interactController.Dispose();
        }
    }
}
