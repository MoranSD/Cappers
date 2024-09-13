namespace Gameplay.QuestSystem.Quests.Factory
{
    public interface IQuestFactory
    {
        Quest CreateQuest(QuestData questData);
    }
}
