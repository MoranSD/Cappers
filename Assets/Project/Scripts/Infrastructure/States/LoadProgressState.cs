using Infrastructure.SceneLoad;
using World.Variants;
using World;
using Infrastructure.Root;
using Gameplay.Game;
using Infrastructure.DataProviding;
using World.Data;

namespace Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine stateMachine;
        private readonly Game game;
        private readonly GameData gameData;
        private readonly IAssetProvider assetProvider;

        public LoadProgressState(GameStateMachine stateMachine, Game game, GameData gameData, IAssetProvider assetProvider)
        {
            this.stateMachine = stateMachine;
            this.game = game;
            this.gameData = gameData;
            this.assetProvider = assetProvider;
        }

        public void Enter()
        {
            var allWorldsConfig = assetProvider.Load<AllWorldsConfig>("Configs/World/AllWorldsConfig");
            var worldConfig = allWorldsConfig.GetWorldConfig(0);//0 is temporary world config

            var gameWorld = worldConfig.CreateWorld();

            gameData.SetWorld(gameWorld);
            gameData.OpenLocation(1);//1 is "Port1" location id

            stateMachine.Enter<LoadLevelState, SceneType>(SceneType.ShipAtSea);
        }

        public void Exit()
        {

        }
    }
}