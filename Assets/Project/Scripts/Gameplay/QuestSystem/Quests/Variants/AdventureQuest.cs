using Gameplay.Game;

namespace Gameplay.QuestSystem.Quests.Variants
{
    public class AdventureQuest : Quest
    {
        public int CompletionLocationId { get; }
        public int ItemLocationId { get; }

        private readonly GameState gameState;

        private bool isItemTaken;

        public AdventureQuest(int itemLocationId, int completionLocationId, GameState gameState, QuestData data) : base(data)
        {
            CompletionLocationId = completionLocationId;
            ItemLocationId = itemLocationId;
            this.gameState = gameState;
        }

        public override bool IsConditionFulfilled()
        {
            return gameState.CurrentLocationId == CompletionLocationId && isItemTaken;
        }

        public override void Initialize()
        {

        }

        public override void Dispose()
        {

        }
    }
}
