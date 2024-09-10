using CameraSystem;
using Infrastructure.TickManagement;
using Player;
using Player.Data;
using Player.View;
using Services.GameInput;
using UnityEngine;

namespace Infrastructure
{
    public class ShipAtSeaRoot : MonoBehaviour
    {
        [SerializeField] private PlayerConfigSO playerConfigSO;
        [SerializeField] private PlayerView playerView;
        [Space]
        [SerializeField] private GameCamera gameCamera;

        private TickManager tickManager;
        private PlayerController player;

        private void Awake()
        {
            ServiceLocator.Register<IGameCamera>(gameCamera);

            var input = ServiceLocator.Get<IInput>();

            tickManager = ServiceLocator.Get<TickManager>();

            player = new PlayerController(playerConfigSO.MainConfig, playerView, input, gameCamera);
            player.Initialize();
            tickManager.Add(player);
        }

        private void OnDestroy()
        {
            ServiceLocator.Remove<GameCamera>();

            tickManager.Remove(player);
            player.Dispose();
        }
    }
}