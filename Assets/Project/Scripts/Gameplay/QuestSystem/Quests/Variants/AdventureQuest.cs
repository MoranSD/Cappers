using Gameplay.Game;
using Gameplay.QuestSystem.Data;
using Gameplay.Ship.Inventory;

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
        private readonly ShipInventory shipInventory;

        public AdventureQuest(int requiredItemId, int itemLocationId, int completionLocationId, GameState gameState, ShipInventory shipInventory, QuestData data) : base(data)
        {
            RequiredItemId = requiredItemId;
            CompletionLocationId = completionLocationId;
            ItemLocationId = itemLocationId;
            this.gameState = gameState;
            this.shipInventory = shipInventory;
        }

        public override bool IsConditionFulfilled()
        {
            return gameState.CurrentLocationId == CompletionLocationId && IsItemTaken;
        }

        public override void Initialize()
        {
            gameState.OpenLocation(CompletionLocationId);
            gameState.OpenLocation(ItemLocationId);

            shipInventory.OnQuestItemTaken += OnTakeQuestItem;
        }

        public override void Complete()
        {
            if (shipInventory.TryGetQuestItem(RequiredItemId) == false)
                throw new System.Exception($"{Data.ToString()} {RequiredItemId}");
        }

        public override void Dispose()
        {
            shipInventory.OnQuestItemTaken -= OnTakeQuestItem;
        }

        private void OnTakeQuestItem(int itemId)
        {
            if (IsItemTaken) return;

            IsItemTaken = RequiredItemId == itemId;
        }
    }
}
