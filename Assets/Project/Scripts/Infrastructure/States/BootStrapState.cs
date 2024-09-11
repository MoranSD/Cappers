using Infrastructure.Root;
using Infrastructure.SceneLoad;
using Infrastructure.TickManagement;
using Infrastructure.GameInput;
using UnityEngine;
using Infrastructure.Panels;
using Infrastructure.Routine;
using Infrastructure.Travel;
using Infrastructure.Composition;
using Infrastructure.DataProviding;
using Gameplay.Game;

namespace Infrastructure.States
{
    public class BootStrapState : IState
    {
        private readonly Game game;
        private readonly GameStateMachine stateMachine;
        private readonly ICoroutineRunner coroutineRunner;

        public BootStrapState(Game game, GameStateMachine stateMachine, ICoroutineRunner coroutineRunner)
        {
            this.game = game;
            this.stateMachine = stateMachine;
            this.coroutineRunner = coroutineRunner;

            RegisterServices();
        }

        public void Enter()
        {
            ServiceLocator.Get<ISceneLoader>().Load(SceneType.Initial, OnLoadInitialScene);
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
            ServiceLocator.Initialize();

            ServiceLocator.Register(game);
            ServiceLocator.Register<IInput>(new PCInput());
            ServiceLocator.Register<ISceneLoader>(new SceneLoader(coroutineRunner));
            ServiceLocator.Register<ICompositionController>(new CompositionController());
            ServiceLocator.Register<IAssetProvider>(new AssetProvider());
            ServiceLocator.Register(new TickManager());
            ServiceLocator.Register(new PanelsManager(PanelType.gameplay, coroutineRunner));
            ServiceLocator.Register(new TravelSystem());
            ServiceLocator.Register(new GameData());
        }
    }
}