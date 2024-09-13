using Gameplay.QuestSystem.Quests;
using System;
using Utils.Interaction;

namespace Gameplay.QuestSystem.Menu.View
{
    public interface IQuestMenuView
    {
        event Action<QuestData> OnSelectQuest;
        event Action OnTryToClose;
        IInteractor Interactor { get; }
        void RedrawQuests(params QuestData[] datas);
    }
}
