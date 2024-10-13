using Gameplay.Player.InteractController;
using Gameplay.QuestSystem.Menu.View;
using Gameplay.QuestSystem.Quests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gameplay.QuestSystem.Menu
{
    public class QuestMenu
    {
        private readonly QuestManager questManager;
        private readonly PlayerMenuInteractController playerMenuInteract;
        private readonly IQuestMenuView view;

        public QuestMenu(QuestManager questManager, PlayerMenuInteractController playerMenuInteract, IQuestMenuView view)
        {
            this.questManager = questManager;
            this.playerMenuInteract = playerMenuInteract;
            this.view = view;
        }

        public void Initialize()
        {
            view.OnPlayerInteract += OnPlayerInteract;
            view.OnTryToClose += OnTryToClose;
            view.OnSelectQuest += OnSelectQuest;
        }

        public void Dispose()
        {
            view.OnPlayerInteract -= OnPlayerInteract;
            view.OnTryToClose -= OnTryToClose;
            view.OnSelectQuest -= OnSelectQuest;
        }

        private void OnSelectQuest(QuestData questData)
        {
            if (playerMenuInteract.CheckInteraction(Panels.PanelType.questMenu) == false)
                throw new Exception();

            if(questManager.HasQuest(questData))
            {
                if(questManager.IsAbleToComplete(questData))
                {
                    questManager.CompleteQuest(questData);
                    RedrawQuests();
                }
            }
            else
            {
                questManager.AddQuest(questData);
                RedrawQuests();
            }
        }

        private void OnPlayerInteract()
        {
            if (playerMenuInteract.CheckInteraction(Panels.PanelType.questMenu))
                throw new Exception();

            RedrawQuests();
            playerMenuInteract.EnterInteractState(Panels.PanelType.questMenu);
        }

        private void OnTryToClose()
        {
            if (playerMenuInteract.CheckInteraction(Panels.PanelType.questMenu) == false)
                throw new Exception();

            playerMenuInteract.ExitInteractState();
        }

        private void RedrawQuests()
        {
            var availableQuests = new List<QuestData>();

            //текущие квесты которые можно сдать
            availableQuests.AddRange(questManager.ActiveQuests.Where(x => x.IsConditionFulfilled()).Select(x => x.Data));
            //квесты локации которые не взяли/выполнили
            availableQuests.AddRange(questManager.GetAvailableLocationQuests());

            view.RedrawQuests(availableQuests.ToArray());
        }
    }
}
