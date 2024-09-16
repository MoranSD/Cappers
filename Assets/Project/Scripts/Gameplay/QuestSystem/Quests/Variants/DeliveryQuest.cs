using Gameplay.Game;
using Gameplay.QuestSystem.Data;

namespace Gameplay.QuestSystem.Quests.Variants
{
    public class DeliveryQuest : Quest
    {
        public int CompletionLocationId { get; }

        public override QuestType QuestType => QuestType.delivery;

        private readonly GameState gameState;

        public DeliveryQuest(GameState gameState, int completionLocationId, QuestData data) : base(data)
        {
            this.gameState = gameState;
            CompletionLocationId = completionLocationId;
        }

        public override void Initialize()
        {
            gameState.OpenLocation(CompletionLocationId);
        }

        public override bool IsConditionFulfilled()
        {
            return gameState.CurrentLocationId == CompletionLocationId;
        }
    }
}
