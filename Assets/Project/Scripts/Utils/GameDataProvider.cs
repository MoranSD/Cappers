using Gameplay.Game;
using Gameplay.World.Data;
using Infrastructure.DataProviding;
using Infrastructure;
using QuestSystem.Quests.Item;

namespace Utils
{
    public static class GameDataProvider
    {
        public static GameWorldConfig GetCurrentWorldConfig()
        {
            var assetProvider = ServiceLocator.Get<IAssetProvider>();
            var allWorldConfig = assetProvider.Load<AllWorldsConfig>(Constants.AllWorldConfigsConfigPath);
            var gameState = ServiceLocator.Get<GameState>();
            return allWorldConfig.GetWorldConfig(gameState.World.Id);
        }

        public static GameWorldConfig GetWorldConfig(int worldId)
        {
            var assetProvider = ServiceLocator.Get<IAssetProvider>();
            var allWorldConfig = assetProvider.Load<AllWorldsConfig>(Constants.AllWorldConfigsConfigPath);
            return allWorldConfig.GetWorldConfig(worldId);
        }

        public static LocationConfig GetLocationConfigInCurrentWorld(int id)
        {
            var worldConfig = GetCurrentWorldConfig();
            return worldConfig.GetLocationConfig(id);
        }

        public static int GetLocationIdInCurrentWorld(LocationConfig locationConfig)
        {
            var worldConfig = GetCurrentWorldConfig();
            return worldConfig.GetLocationId(locationConfig);
        }

        public static int GetQuestItemIdInCurrentWorld(QuestItemConfig questItemConfig)
        {
            var assetProvider = ServiceLocator.Get<IAssetProvider>();
            var gameState = ServiceLocator.Get<GameState>();
            var worldQuestItemsConfig = assetProvider.Load<WorldQuestItemsConfig>(string.Format(Constants.WorldQuestItemsConfigPathFormat, gameState.World.Id));
            return worldQuestItemsConfig.GetItemId(questItemConfig);
        }
    }
}
