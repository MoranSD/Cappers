using Infrastructure.SceneLoad;
using World.Variants;
using World;
using Infrastructure.Root;
using Gameplay.Game;

namespace Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine stateMachine;
        private readonly Game game;
        private readonly GameData gameData;

        public LoadProgressState(GameStateMachine stateMachine, Game game, GameData gameData)
        {
            this.stateMachine = stateMachine;
            this.game = game;
            this.gameData = gameData;
        }

        public void Enter()
        {
            var port1Location = new PortLocation(0, "Port 1");
            var port2Location = new PortLocation(1, "Port 2");
            var gameWorld = new GameWorld(
                0, 
                new Location[]
                {
                    port1Location,
                    port2Location
                }
            );

            gameData.SetWorld(gameWorld);
            gameData.OpenLocation(port1Location.Id);

            stateMachine.Enter<LoadLevelState, SceneType>(SceneType.ShipAtSea);
        }

        public void Exit()
        {

        }
    }
}