using Gameplay.CameraSystem;
using Gameplay.Player.Data;
using Gameplay.Player.InteractController;
using Gameplay.Player.View;
using Infrastructure;
using Infrastructure.Composition;
using Infrastructure.DataProviding;
using Infrastructure.GameInput;
using Infrastructure.Panels;
using Infrastructure.TickManagement;
using UnityEngine;

namespace Gameplay.Player.Root
{
    public class PlayerInstaller : Installer
    {
        [SerializeField] private PlayerView playerView;
        [Space]
        [SerializeField] private GameCamera gameCamera;

        private TickManager tickManager;
        private PlayerController player;

        public override void Initialize()
        {
            var input = ServiceLocator.Get<IInput>();
            var assetProvider = ServiceLocator.Get<IAssetProvider>();
            tickManager = ServiceLocator.Get<TickManager>();

            var playerConfig = assetProvider.Load<PlayerConfigSO>(Constants.PlayerConfigPath);

            gameCamera.Initialize();

            player = new PlayerController(playerConfig.MainConfig, playerView, input, gameCamera);
            player.Initialize();
            tickManager.Add(player);

            var panelsManager = ServiceLocator.Get<PanelsManager>();
            ServiceLocator.Register(new PlayerMenuInteractController(player, panelsManager, gameCamera));
        }

        public override void Dispose()
        {
            tickManager.Remove(player);
            player.Dispose();
            ServiceLocator.Remove<PlayerMenuInteractController>();
        }
    }
}
