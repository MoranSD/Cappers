using QuestSystem.Quests.Item;

namespace Gameplay.QuestSystem.Quests.Item.Spawn.Factory
{
    public interface IQuestItemFactory
    {
        QuestItem Create(int worldId, int itemId);
    }
}
