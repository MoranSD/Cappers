using Gameplay.Ship.Inventory;
using Infrastructure;
using Infrastructure.DataProviding;
using QuestSystem.Quests.Item;
using UnityEngine;

namespace Gameplay.QuestSystem.Quests.Item.Spawn.Factory
{
    public class QuestItemFactory : IQuestItemFactory
    {
        private readonly IAssetProvider assetProvider;
        private readonly ShipInventory shipInventory;

        public QuestItemFactory(IAssetProvider assetProvider, ShipInventory shipInventory)
        {
            this.assetProvider = assetProvider;
            this.shipInventory = shipInventory;
        }

        public QuestItem Create(int worldId, int itemId)
        {
            var allQuestItemsConfig = assetProvider.Load<WorldQuestItemsConfig>(string.Format(Constants.WorldQuestItemsConfigPathFormat, worldId));
            var questItemConfig = allQuestItemsConfig.GetConfig(itemId);
            var questItemViewPrefab = questItemConfig.ViewPrefab;

            var questItemView = Object.Instantiate(questItemViewPrefab, questItemConfig.WorldPosition, questItemConfig.WorldRotation);
            return new QuestItem(itemId, shipInventory, questItemView);
        }
    }
}
