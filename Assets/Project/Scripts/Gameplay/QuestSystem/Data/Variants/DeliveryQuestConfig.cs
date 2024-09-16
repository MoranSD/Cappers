using Gameplay.Game;
using Gameplay.QuestSystem.Quests;
using Gameplay.QuestSystem.Quests.Variants;
using Gameplay.World.Data;
using Infrastructure;
using UnityEngine;
using Utils;

namespace Gameplay.QuestSystem.Data.Variants
{
    [CreateAssetMenu(menuName = "Quests/Variants/DeliveryQuestConfig")]
    public class DeliveryQuestConfig : QuestConfig
    {
        public LocationConfig CompletionLocation;

        public override QuestType QuestType => QuestType.delivery;

        [SerializeField, TextArea()] private string descriptionFormat;

        public override Quest CreateQuest(QuestData questData)
        {
            var gameState = ServiceLocator.Get<GameState>();
            var completionLocationId = GameDataProvider.GetLocationIdInCurrentWorld(CompletionLocation);
            return new DeliveryQuest(gameState, completionLocationId, questData);
        }

        public override string GetDescription()
        {
            var completionLocationId = GameDataProvider.GetLocationIdInCurrentWorld(CompletionLocation);
            var completionLocationName = CompletionLocation.LocationName;
            return string.Format(descriptionFormat, completionLocationName);
        }
    }
}
