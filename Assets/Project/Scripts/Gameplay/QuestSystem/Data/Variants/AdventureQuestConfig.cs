using Gameplay.Game;
using Gameplay.QuestSystem.Quests;
using Gameplay.QuestSystem.Quests.Variants;
using Gameplay.Ship.Inventory;
using Gameplay.World.Data;
using Infrastructure;
using QuestSystem.Quests.Item;
using UnityEngine;
using Utils;

namespace Gameplay.QuestSystem.Data.Variants
{
    [CreateAssetMenu(menuName = "Quests/Variants/AdventureQuestConfig")]
    public class AdventureQuestConfig : QuestConfig
    {
        public QuestItemConfig RequiredItem;
        public LocationConfig ItemLocation;
        public LocationConfig CompletionLocation;

        public override QuestType QuestType => QuestType.adventure;

        [SerializeField, TextArea()] private string descriptionFormat;

        public override Quest CreateQuest(QuestData questData)
        {
            var gameState = ServiceLocator.Get<GameState>();
            int itemLocationId = GameDataProvider.GetLocationIdInCurrentWorld(ItemLocation);
            int completionLocationId = GameDataProvider.GetLocationIdInCurrentWorld(CompletionLocation);
            int requiredItemId = GameDataProvider.GetQuestItemIdInCurrentWorld(RequiredItem);
            var shipInventory = ServiceLocator.Get<ShipInventory>();

            return new AdventureQuest(requiredItemId, itemLocationId, completionLocationId, gameState, shipInventory, questData);
        }

        public override string GetDescription()
        {
            int itemLocationId = GameDataProvider.GetLocationIdInCurrentWorld(ItemLocation);
            int completionLocationId = GameDataProvider.GetLocationIdInCurrentWorld(CompletionLocation);

            var itemLocationName = GameDataProvider.GetLocationConfigInCurrentWorld(itemLocationId).LocationName;
            var completionLocationName = GameDataProvider.GetLocationConfigInCurrentWorld(completionLocationId).LocationName;

            return string.Format(descriptionFormat, itemLocationName, RequiredItem.ItemName, completionLocationName);
        }
    }
}
