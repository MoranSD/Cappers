using Gameplay.QuestSystem.Quests;
using Gameplay.QuestSystem.Quests.Variants;
using UnityEngine;

namespace Gameplay.QuestSystem.Data.Variants
{
    [CreateAssetMenu(menuName = "Quests/DeliveryQuestConfig")]
    public class DeliveryQuestConfig : QuestConfig
    {
        public int CompletionLocationId;

        public override Quest CreateQuest()
        {
            return new DeliveryQuest(CompletionLocationId, QuestData);
        }
    }
}
