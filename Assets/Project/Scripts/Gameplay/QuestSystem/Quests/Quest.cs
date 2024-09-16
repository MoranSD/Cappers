using Gameplay.QuestSystem.Data;

namespace Gameplay.QuestSystem.Quests
{
    public abstract class Quest
    {
        public abstract QuestType QuestType { get; }
        public readonly QuestData Data;

        public Quest(QuestData data)
        {
            Data = data;
        }

        public abstract bool IsConditionFulfilled();
        public virtual void Initialize() { }
        public virtual void Dispose() { }
    }
}
