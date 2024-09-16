using Gameplay.Game;
using Gameplay.QuestSystem.Quests;
using Gameplay.QuestSystem.Quests.Factory;
using Gameplay.World.Variants.Port;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gameplay.QuestSystem
{
    public class QuestManager
    {
        public IReadOnlyList<Quest> ActiveQuests => activeQuests;

        private readonly GameState gameState;
        private readonly IQuestFactory questFactory;

        private List<Quest> activeQuests;

        public QuestManager(GameState gameState, IQuestFactory questFactory)
        {
            this.gameState = gameState;
            this.questFactory = questFactory;
            activeQuests = new List<Quest>();
        }

        //возьмет из GameState айдишники квестов, передаст в фабрику и добавит в себя
        public void InitializeFromState()
        {

        }

        public void Dispose()
        {
            foreach (Quest quest in activeQuests)
                quest.Dispose();
        }

        public QuestData[] GetAvailableLocationQuests()
        {
            var currentLocationId = gameState.CurrentLocationId;

            if (gameState.World.HasLocation(currentLocationId) == false)
                throw new Exception(currentLocationId.ToString());

            var currentLocation = gameState.World.GetLocation(currentLocationId);

            /*
            //в будущем, если задания будут не только в порту, 
            //то можно тупо добавить расширение классу локации
            //в виде интерфейса
             */
            if (currentLocation.Type != World.Data.LocationType.port)
                throw new Exception(currentLocationId.ToString());

            var availableQuests = ((PortLocation)currentLocation).Quests.ToList();

            for (int i = availableQuests.Count - 1; i >= 0; i--)
            {
                var locationQuest = availableQuests[i];

                if (gameState.CurrentQuests.Any(x => x.Compare(locationQuest)))
                    availableQuests.RemoveAt(i);

                if (gameState.CompletedQuests.Any(x => x.Compare(locationQuest)))
                    availableQuests.RemoveAt(i);
            }

            return availableQuests.ToArray();
        }

        public bool IsAbleToComplete(QuestData questData)
        {
            if(HasQuest(questData) == false)
                throw new Exception(questData.ToString());

            var targetQuest = activeQuests.First(x => x.Data.Compare(questData));

            return targetQuest.IsConditionFulfilled();
        }
        public bool HasQuest(QuestData questData)
        {
            var targetQuest = activeQuests.FirstOrDefault(x => x.Data.Compare(questData));

            return targetQuest != null;
        }

        public void AddQuest(QuestData questData)
        {
            AddQuestToState(questData);
            var quest = questFactory.CreateQuest(questData);
            quest.Initialize();
            activeQuests.Add(quest);
        }
        public void CompleteQuest(QuestData questData)
        {
            var targetQuest = activeQuests.FirstOrDefault(x => x.Data.Compare(questData));

            if (targetQuest == null || targetQuest.IsConditionFulfilled() == false)
                throw new Exception($"{targetQuest.ToString()}:{questData.ToString()}");

            CompleteQuestInState(questData);

            activeQuests.Remove(targetQuest);
            targetQuest.Dispose();
        }

        private void AddQuestToState(QuestData questData)
        {
            if (gameState.CurrentQuests.Any(x => x.Compare(questData)))
                throw new Exception(questData.ToString());

            if (gameState.CompletedQuests.Any(x => x.Compare(questData)))
                throw new Exception(questData.ToString());

            gameState.CurrentQuests.Add(questData);
        }
        private void CompleteQuestInState(QuestData questData)
        {
            if (gameState.CurrentQuests.Any(x => x.Compare(questData)) == false)
                throw new Exception(questData.ToString());

            if (gameState.CompletedQuests.Any(x => x.Compare(questData)))
                throw new Exception(questData.ToString());

            gameState.CurrentQuests.Remove(questData);
            gameState.CompletedQuests.Add(questData);
        }
    }
}
