using Infrastructure.SceneLoad;
using Infrastructure.TickManagement;
using Infrastructure.GameInput;
using UnityEngine;
using Gameplay.Panels;
using Gameplay.Travel;
using Infrastructure.Composition;
using Infrastructure.DataProviding;
using Gameplay.Game;
using Gameplay.LevelLoad;
using Gameplay.QuestSystem;
using Gameplay.QuestSystem.Quests.Factory;
using Gameplay.World.Data;
using QuestSystem.Quests.Item.Spawn;
using Gameplay.QuestSystem.Quests.Item.Spawn.Factory;
using Gameplay.Ship.Inventory;

namespace Infrastructure.States
{
    public class BootStrapState : IState
    {
        private readonly GameStateMachine stateMachine;
        private readonly Transform mainObjectTF;

        public BootStrapState(GameStateMachine stateMachine, Transform mainObjectTF)
        {
            this.stateMachine = stateMachine;
            this.mainObjectTF = mainObjectTF;
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
            var sceneLoader = ServiceLocator.Register<ISceneLoader>(new SceneLoader());
            ((SceneLoader)sceneLoader).Initialize();
            var compositionController = ServiceLocator.Register<ICompositionController>(new CompositionController());
            var assetProvider = ServiceLocator.Register<IAssetProvider>(new AssetProvider());
            var panelsManager = ServiceLocator.Register(new PanelsManager(PanelType.gameplay));
            panelsManager.Initialize();
            var curtainPanelPrefab = assetProvider.Load<CurtainPanel>(Constants.CurtainPanelPrefabPath);
            var curtainPanel = ServiceLocator.Register(Object.Instantiate(curtainPanelPrefab, mainObjectTF));
            panelsManager.RegisterPanel(curtainPanel);
            var gameState = ServiceLocator.Register(new GameState());
            var levelLoadService = ServiceLocator.Register<ILevelLoadService>
                (new LevelLoadService(panelsManager, sceneLoader, compositionController, gameState, assetProvider));
            ((LevelLoadService)levelLoadService).Initialize();
            var travelSystem = ServiceLocator.Register(new TravelSystem(gameState, levelLoadService));
            travelSystem.Initialize();
            var questFactory = new QuestFactory(assetProvider.Load<AllWorldsConfig>(Constants.AllWorldConfigsConfigPath), gameState);
            var questManager = ServiceLocator.Register(new QuestManager(gameState, questFactory));
            var shipInventory = ServiceLocator.Register(new ShipInventory(gameState));
            var questItemFactory = new QuestItemFactory(assetProvider, shipInventory);
            var questItemSpawnSystem = ServiceLocator.Register(new QuestItemSpawnSystem(gameState, questManager, travelSystem, questItemFactory));
            questItemSpawnSystem.Initialize();
        }
    }
}