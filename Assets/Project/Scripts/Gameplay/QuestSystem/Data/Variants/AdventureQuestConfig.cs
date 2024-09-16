using Gameplay.Game;
using Gameplay.QuestSystem.Quests;
using Gameplay.QuestSystem.Quests.Variants;
using Gameplay.World.Data;
using Infrastructure;
using UnityEngine;
using Utils;

namespace Gameplay.QuestSystem.Data.Variants
{
    [CreateAssetMenu(menuName = "Quests/AdventureQuestConfig")]
    public class AdventureQuestConfig : QuestConfig
    {
        public LocationConfig ItemLocation;
        public LocationConfig CompletionLocation;

        [SerializeField, TextArea()] private string descriptionFormat;

        public override Quest CreateQuest(QuestData questData)
        {
            var gameState = ServiceLocator.Get<GameState>();
            int itemLocationId = 0;
            int completionLocationId = 0;
            return new AdventureQuest(itemLocationId, completionLocationId, gameState, questData);
        }

        public override string GetDescription()
        {
            int itemLocationId = GameDataProvider.GetLocationIdInCurrentWorld(ItemLocation);
            int completionLocationId = GameDataProvider.GetLocationIdInCurrentWorld(CompletionLocation);

            var itemLocationName = GameDataProvider.GetLocationConfigInCurrentWorld(itemLocationId).LocationName;
            var completionLocationName = GameDataProvider.GetLocationConfigInCurrentWorld(completionLocationId).LocationName;

            return string.Format(descriptionFormat, itemLocationName, completionLocationName);
        }
    }
}
