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
using Gameplay.LevelLoad;
using Infrastructure.Curtain;

namespace Infrastructure.States
{
    public class BootStrapState : IState
    {
        private readonly GameStateMachine stateMachine;
        private readonly ICoroutineRunner coroutineRunner;
        private readonly ILoadingCurtain loadingCurtain;

        public BootStrapState(GameStateMachine stateMachine, ICoroutineRunner coroutineRunner, ILoadingCurtain loadingCurtain)
        {
            this.stateMachine = stateMachine;
            this.coroutineRunner = coroutineRunner;
            this.loadingCurtain = loadingCurtain;
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

            var tickManager = ServiceLocator.Register(new TickManager());
            var input = ServiceLocator.Register<IInput>(new PCInput());
            tickManager.Add(input as ITickable);
            var sceneLoader = ServiceLocator.Register<ISceneLoader>(new SceneLoader(coroutineRunner));
            var compositionController = ServiceLocator.Register<ICompositionController>(new CompositionController());
            var assetProvider = ServiceLocator.Register<IAssetProvider>(new AssetProvider());
            ServiceLocator.Register(new PanelsManager(PanelType.gameplay, coroutineRunner));
            var gameData = ServiceLocator.Register(new GameData());
            var levelLoadService = ServiceLocator.Register<ILevelLoadService>
                (new LevelLoadService(loadingCurtain, sceneLoader, compositionController, gameData, assetProvider));
            ServiceLocator.Register(new TravelSystem(gameData, levelLoadService, coroutineRunner));
        }
    }
}