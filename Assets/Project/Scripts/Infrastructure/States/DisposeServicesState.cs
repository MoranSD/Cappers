using Infrastructure.TickManagement;
using Infrastructure.GameInput;
using Infrastructure.Composition;
using Gameplay.Panels;
using Gameplay.Travel;
using Infrastructure.SceneLoad;
using Infrastructure.DataProviding;
using Gameplay.Game;
using Gameplay.LevelLoad;
using Gameplay.QuestSystem;
using QuestSystem.Quests.Item.Spawn;
using Gameplay.Ship.Inventory;

namespace Infrastructure.States
{
    public class DisposeServicesState : IState
    {
        private readonly ICompositionController compositionController;

        public DisposeServicesState(ICompositionController compositionController)
        {
            this.compositionController = compositionController;
        }

        public void Enter()
        {
            ClearServices();
        }

        public void Exit()
        {

        }

        private void ClearServices()
        {
            compositionController.Dispose();

            var input = ServiceLocator.Remove<IInput>();
            var sceneLoader = ServiceLocator.Remove<ISceneLoader>() as SceneLoader;
            sceneLoader.Dispose();
            ServiceLocator.Remove<ICompositionController>();
            ServiceLocator.Remove<IAssetProvider>();
            var tickManager = ServiceLocator.Remove<TickManager>();
            tickManager.Remove(input as ITickable);
            var panelsManager = ServiceLocator.Remove<PanelsManager>();
            var curtainPanel = ServiceLocator.Remove<CurtainPanel>();
            panelsManager.UnregisterPanel(curtainPanel);
            panelsManager.Dispose();
            var travelSystem = ServiceLocator.Remove<TravelSystem>();
            travelSystem.Dispose();
            var levelLoadService = ServiceLocator.Remove<ILevelLoadService>() as LevelLoadService;
            levelLoadService.Dispose();
            ServiceLocator.Remove<GameState>();
            var questManager = ServiceLocator.Remove<QuestManager>();
            questManager.Dispose();
            var questItemSpawnSystem = ServiceLocator.Remove<QuestItemSpawnSystem>();
            questItemSpawnSystem.Dispose();
            ServiceLocator.Remove<ShipInventory>();

            ServiceLocator.Clear();
        }
    }
}