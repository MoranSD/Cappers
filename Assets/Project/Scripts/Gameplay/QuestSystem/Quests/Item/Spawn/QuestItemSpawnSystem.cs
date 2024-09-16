using Gameplay.Game;
using Gameplay.QuestSystem;
using Gameplay.QuestSystem.Data;
using Gameplay.QuestSystem.Quests.Item.Spawn.Factory;
using Gameplay.QuestSystem.Quests.Variants;
using Gameplay.Travel;
using System.Collections.Generic;
using System.Linq;

namespace QuestSystem.Quests.Item.Spawn
{
    public class QuestItemSpawnSystem
    {
        private readonly GameState gameState;
        private readonly QuestManager questManager;
        private readonly TravelSystem travelSystem;
        private readonly IQuestItemFactory questItemFactory;

        private List<QuestItem> activeItems;

        public QuestItemSpawnSystem(GameState gameState, QuestManager questManager, TravelSystem travelSystem, IQuestItemFactory questItemFactory)
        {
            this.gameState = gameState;
            this.questManager = questManager;
            this.travelSystem = travelSystem;
            this.questItemFactory = questItemFactory;
            activeItems = new List<QuestItem>();
        }

        public void Initialize()
        {
            travelSystem.OnLeaveLocation += DisposeActiveItems;
            travelSystem.OnArriveToLocation += OnLocationChanged;
        }

        public void Dispose()
        {
            DisposeActiveItems();

            travelSystem.OnLeaveLocation -= DisposeActiveItems;
            travelSystem.OnArriveToLocation -= OnLocationChanged;
        }

        private void DisposeActiveItems()
        {
            foreach (var item in activeItems)
                item.Dispose();

            activeItems.Clear();
        }

        private void OnLocationChanged()
        {
            var adventureQuests = questManager.ActiveQuests.Where(x => x.QuestType == QuestType.adventure);

            for (int i = 0; i < adventureQuests.Count(); i++)
            {
                var quest = (AdventureQuest)adventureQuests.ElementAt(i);

                if (quest.ItemLocationId != gameState.CurrentLocationId)
                    continue;

                if (quest.IsItemTaken)
                    continue;

                var questItem = questItemFactory.Create(gameState.World.Id, quest.RequiredItemId);
                questItem.Initialize();
                activeItems.Add(questItem);
            }
        }
    }
}
