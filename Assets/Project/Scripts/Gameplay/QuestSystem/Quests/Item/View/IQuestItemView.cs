using Utils.Interaction;

namespace QuestSystem.Quests.Item.View
{
    public interface IQuestItemView
    {
        IInteractor Interactor { get; }
        void Destroy();
    }
}
