using Gameplay.QuestSystem.Quests;
using System;

namespace Gameplay.QuestSystem.Menu.View
{
    public interface IQuestMenuView
    {
        event Action<QuestData> OnSelectQuest;
        event Action OnTryToClose;
        event Action OnPlayerInteract;
        void RedrawQuests(params QuestData[] datas);
    }
}
