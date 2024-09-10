using Infrastructure.SceneLoad;
using Infrastructure.TickManagement;
using Infrastructure.GameInput;
using Infrastructure.Map;
using UnityEngine;
using World.Variants;
using World;
using Infrastructure.Root;

namespace Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine stateMachine;
        private readonly Game game;
        private readonly WorldMapService worldMap;

        public LoadProgressState(GameStateMachine stateMachine, Game game, WorldMapService worldMap)
        {
            this.stateMachine = stateMachine;
            this.game = game;
            this.worldMap = worldMap;
        }

        public void Enter()
        {
            var port1Location = new PortLocation("Port 1", Vector2.zero);
            var port2Location = new PortLocation("Port 2", Vector2.one);
            var gameWorld = new GameWorld(new Location[]
            {
                port1Location,
                port2Location
            });
            game.SetWorld(gameWorld);
            worldMap.AddLocation(port1Location);

            stateMachine.Enter<LoadLevelState, SceneType>(SceneType.ShipAtSea);
        }

        public void Exit()
        {

        }
    }
}