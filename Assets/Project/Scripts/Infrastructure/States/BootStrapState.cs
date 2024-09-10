using Infrastructure.Root;
using Infrastructure.SceneLoad;
using Infrastructure.TickManagement;
using Infrastructure.GameInput;
using Infrastructure.Map;
using UnityEngine;
using World.Variants;
using World;

namespace Infrastructure.States
{
    public class BootStrapState : IState
    {
        private readonly Game game;
        private readonly GameStateMachine stateMachine;
        private readonly ISceneLoader sceneLoader;

        public BootStrapState(Game game, GameStateMachine stateMachine, ISceneLoader sceneLoader)
        {
            this.game = game;
            this.stateMachine = stateMachine;
            this.sceneLoader = sceneLoader;

            RegisterServices();
        }

        public void Enter()
        {
            sceneLoader.Load(SceneType.Initial, OnLoadInitialScene);
        }

        public void Exit()
        {

        }

        private void OnLoadInitialScene()
        {
            stateMachine.Enter<LoadProgressState>();
        }

        private void RegisterServices()
        {
            Application.targetFrameRate = 60;

            ServiceLocator.Register(game);
            ServiceLocator.Register<IInput>(new PCInput());
            ServiceLocator.Register(new TickManager());

            ServiceLocator.Register(new WorldMapService());
        }
    }
}