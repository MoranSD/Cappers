using Gameplay.QuestSystem.Quests;
using System;

namespace Gameplay.QuestSystem.Menu.View
{
    public interface IQuestMenuView
    {
        event Action<QuestData> OnSelectQuest;
        event Action<QuestData> OnCompleteQuest;
        event Action OnTryToClose;
        event Action OnPlayerInteract;

        void DrawAvailableQuests(QuestData[] questDatas);
        void DrawCompleteableQuests(QuestData[] questDatas);
        void DrawSelectSuccess(QuestData questData);
        void DrawCompleteSuccess(QuestData questData);
    }
}
