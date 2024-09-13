using Gameplay.Game;
using Gameplay.Panels;
using Gameplay.Player.InteractController;
using Gameplay.QuestSystem.Menu;
using Gameplay.QuestSystem.Menu.Factory;
using Gameplay.QuestSystem.Menu.View;
using Gameplay.World.Data;
using Infrastructure;
using Infrastructure.Composition;
using Infrastructure.DataProviding;
using UnityEngine;

namespace Gameplay.QuestSystem.Root
{
    public class QuestInstaller : Installer
    {
        [SerializeField] private QuestMenuView questMenuView;
        [SerializeField] private QuestWidget questWidgetPrefab;

        private QuestMenu questMenu;
        private PanelsManager panelsManager;

        public override void Initialize()
        {
            panelsManager = ServiceLocator.Get<PanelsManager>();
            panelsManager.RegisterPanel(questMenuView);

            var gameState = ServiceLocator.Get<GameState>();
            var playerMenuInteract = ServiceLocator.Get<PlayerMenuInteractController>();
            var questManager = ServiceLocator.Get<QuestManager>();

            questMenu = new QuestMenu(questManager, playerMenuInteract, questMenuView);

            var assetProvider = ServiceLocator.Get<IAssetProvider>();
            var allWorldsConfig = assetProvider.Load<AllWorldsConfig>(Constants.AllWorldConfigsConfigPath);
             
            var widgetFactory = new QuestWidgetFactory(allWorldsConfig.GetWorldConfig(gameState.World.Id), questWidgetPrefab);

            questMenuView.Initialize(widgetFactory);
            questMenu.Initialize();
        }

        public override void Dispose()
        {
            questMenu.Dispose();
            questMenuView.Dispose();
            panelsManager.UnregisterPanel(questMenuView);
        }
    }
}
