using Gameplay.Game;

namespace Gameplay.QuestSystem.Quests.Variants
{
    public class DeliveryQuest : Quest
    {
        public int CompletionLocationId { get; }

        private readonly GameState gameState;

        public DeliveryQuest(GameState gameState, int completionLocationId, QuestData data) : base(data)
        {
            this.gameState = gameState;
            CompletionLocationId = completionLocationId;
        }

        public override void Initialize()
        {
            if(gameState.OpenedLocations.Contains(CompletionLocationId) == false)
                gameState.OpenedLocations.Add(CompletionLocationId);
        }

        public override bool IsConditionFulfilled()
        {
            return gameState.CurrentLocationId == CompletionLocationId;
        }
    }
}
