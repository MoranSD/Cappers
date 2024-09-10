using Gameplay.CameraSystem;
using Gameplay.Player.Data;
using Gameplay.Player.View;
using Infrastructure;
using Infrastructure.Composition;
using Infrastructure.GameInput;
using Infrastructure.TickManagement;
using UnityEngine;

namespace Gameplay.Player.Root
{
    public class PlayerInstaller : Installer
    {
        [SerializeField] private PlayerConfigSO playerConfigSO;
        [SerializeField] private PlayerView playerView;
        [Space]
        [SerializeField] private GameCamera gameCamera;

        private TickManager tickManager;
        private PlayerController player;

        public override void Initialize()
        {
            var input = ServiceLocator.Get<IInput>();
            tickManager = ServiceLocator.Get<TickManager>();

            player = new PlayerController(playerConfigSO.MainConfig, playerView, input, gameCamera);
            player.Initialize();
            tickManager.Add(player);
        }

        public override void Dispose()
        {
            tickManager.Remove(player);
            player.Dispose();
        }
    }
}
