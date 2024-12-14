using Gameplay.Player.InteractController;
using Gameplay.QuestSystem.Menu.View;
using Gameplay.QuestSystem.Quests;
using System;
using System.Linq;

namespace Gameplay.QuestSystem.Menu
{
    public class QuestMenu
    {
        private readonly QuestManager questManager;
        private readonly PlayerInteractController playerMenuInteract;
        private readonly IQuestMenuView view;

        public QuestMenu(QuestManager questManager, PlayerInteractController playerMenuInteract, IQuestMenuView view)
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
            view.OnCompleteQuest += OnCompleteQuest;
        }

        public void Dispose()
        {
            view.OnPlayerInteract -= OnPlayerInteract;
            view.OnTryToClose -= OnTryToClose;
            view.OnSelectQuest -= OnSelectQuest;
            view.OnCompleteQuest -= OnCompleteQuest;
        }

        private void OnSelectQuest(QuestData questData)
        {
            if (playerMenuInteract.CheckInteraction(Panels.PanelType.questMenu) == false)
                throw new Exception();

            questManager.AddQuest(questData);
            view.DrawSelectSuccess(questData);
        }

        private void OnCompleteQuest(QuestData questData)
        {
            if (playerMenuInteract.CheckInteraction(Panels.PanelType.questMenu) == false)
                throw new Exception();

            if (questManager.IsAbleToComplete(questData))
            {
                questManager.CompleteQuest(questData);
                view.DrawCompleteSuccess(questData);
            }
        }

        private void OnPlayerInteract()
        {
            if (playerMenuInteract.CheckInteraction(Panels.PanelType.questMenu))
                throw new Exception();

            var completeable = questManager.ActiveQuests.Where(x => x.IsConditionFulfilled()).Select(x => x.Data);
            view.DrawCompleteableQuests(completeable.ToArray());

            var available = questManager.GetAvailableLocationQuests();
            view.DrawAvailableQuests(available.ToArray());
            playerMenuInteract.EnterInteractState(Panels.PanelType.questMenu);
        }

        private void OnTryToClose()
        {
            if (playerMenuInteract.CheckInteraction(Panels.PanelType.questMenu) == false)
                throw new Exception();

            playerMenuInteract.ExitInteractState();
        }
    }
}
