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
        private readonly GameState gameData;
        private readonly IAssetProvider assetProvider;

        public LoadProgressState(ILevelLoadService levelLoadService, Game game, GameState gameData, IAssetProvider assetProvider)
        {
            this.levelLoadService = levelLoadService;
            this.game = game;
            this.gameData = gameData;
            this.assetProvider = assetProvider;
        }

        public void Enter()
        {
            gameData.World = CreateWorld(0);
            gameData.CurrentLocationId = GameConstants.SeaLocationId;
            gameData.OpenLocation(0);//0 is "Port 0" location id, depends on index in config

            levelLoadService.LoadLocation(gameData.CurrentLocationId);
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