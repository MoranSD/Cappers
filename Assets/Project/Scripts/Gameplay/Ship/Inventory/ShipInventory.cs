using Gameplay.Game;
using System;

namespace Gameplay.Ship.Inventory
{
    public class ShipInventory
    {
        public event Action<int> OnQuestItemTaken;

        private readonly GameState gameState;

        public ShipInventory(GameState gameState)
        {
            this.gameState = gameState;
        }

        public void AddQuestItem(int itemId)
        {
            gameState.QuestItems.Add(itemId);
            OnQuestItemTaken?.Invoke(itemId);
        }

        public bool TryGetQuestItem(int itemId)
        {
            return gameState.QuestItems.Remove(itemId);
        }
    }
}
