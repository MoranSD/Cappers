namespace Gameplay.QuestSystem.Quests.Variants
{
    public class DeliveryQuest : Quest
    {
        public int CompletionLocationId { get; }

        public DeliveryQuest(int completionLocationId, QuestData data) : base(data)
        {
            CompletionLocationId = completionLocationId;
        }
    }
}
