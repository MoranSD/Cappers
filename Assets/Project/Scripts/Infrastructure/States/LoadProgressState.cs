using Infrastructure.Root;
using Gameplay.Game;
using Infrastructure.DataProviding;
using World.Data;
using Gameplay.LevelLoad;

namespace Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly ILevelLoadService levelLoadService;
        private readonly Game game;
        private readonly GameData gameData;
        private readonly IAssetProvider assetProvider;

        public LoadProgressState(ILevelLoadService levelLoadService, Game game, GameData gameData, IAssetProvider assetProvider)
        {
            this.levelLoadService = levelLoadService;
            this.game = game;
            this.gameData = gameData;
            this.assetProvider = assetProvider;
        }

        public void Enter()
        {
            var allWorldsConfig = assetProvider.Load<AllWorldsConfig>(Constants.AllWorldConfigsConfigPath);
            var worldConfig = allWorldsConfig.GetWorldConfig(0);//0 is temporary world config

            var gameWorld = worldConfig.CreateWorld();

            gameData.World = gameWorld;
            gameData.CurrentLocationId = Constants.SeaLocationId;
            gameData.OpenedLocations.Add(1);//1 is "Port1" location id

            levelLoadService.LoadLocation(gameData.CurrentLocationId);
        }

        public void Exit()
        {

        }
    }
}