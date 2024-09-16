using Gameplay.Game;
using Gameplay.QuestSystem.Data;

namespace Gameplay.QuestSystem.Quests.Variants
{
    public class AdventureQuest : Quest
    {
        public int CompletionLocationId { get; }
        public int ItemLocationId { get; }
        public int RequiredItemId { get; }

        public bool IsItemTaken { get; private set; }

        public override QuestType QuestType => QuestType.adventure;

        private readonly GameState gameState;

        public AdventureQuest(int requiredItemId, int itemLocationId, int completionLocationId, GameState gameState, QuestData data) : base(data)
        {
            RequiredItemId = requiredItemId;
            CompletionLocationId = completionLocationId;
            ItemLocationId = itemLocationId;
            this.gameState = gameState;
        }

        public override bool IsConditionFulfilled()
        {
            return gameState.CurrentLocationId == CompletionLocationId && IsItemTaken;
        }

        public override void Initialize()
        {
            gameState.OpenLocation(CompletionLocationId);
            gameState.OpenLocation(ItemLocationId);
        }

        public override void Dispose()
        {

        }

        private void OnTakeQuestItem(int itemId)
        {
            if (IsItemTaken) return;

            IsItemTaken = RequiredItemId == itemId;
        }
    }
}
