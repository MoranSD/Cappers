namespace Gameplay.QuestSystem.Quests
{
    public abstract class Quest
    {
        public readonly QuestData Data;

        public bool ConditionFulfilled { get; protected set; }

        public Quest(QuestData data)
        {
            Data = data;

            ConditionFulfilled = false;
        }

        public virtual void Initialize() { }
        public virtual void Dispose() { }
    }
}
