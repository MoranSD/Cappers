using Infrastructure.Root;
using Gameplay.Game;
using Infrastructure.DataProviding;
using Gameplay.LevelLoad;
using Gameplay.World;
using Utils;

namespace Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly ILevelLoadService levelLoadService;
        private readonly Game game;
        private readonly GameState gameState;
        private readonly IAssetProvider assetProvider;

        public LoadProgressState(ILevelLoadService levelLoadService, Game game, GameState gameState, IAssetProvider assetProvider)
        {
            this.levelLoadService = levelLoadService;
            this.game = game;
            this.gameState = gameState;
            this.assetProvider = assetProvider;
        }

        public void Enter()
        {
            gameState.World = CreateWorld(0);
            gameState.CurrentLocationId = 0;// GameConstants.SeaLocationId;
            gameState.ShipHealth = 100;
            gameState.OpenLocation(0);//0 is "Port 0" location id, depends on index in config

            levelLoadService.LoadLocation(gameState.CurrentLocationId);
        }

        public void Exit()
        {

        }

        private GameWorld CreateWorld(int worldId)
        {
            var currentWorldConfig = GameDataProvider.GetWorldConfig(worldId);

            var locations = new Location[currentWorldConfig.LocationsCount];
            for (int i = 0; i < locations.Length; i++)
                locations[i] = currentWorldConfig.GetLocationConfig(i).CreateLocation(i);

            var gameWorld = new GameWorld(worldId, locations);
            return gameWorld;
        }
    }
}