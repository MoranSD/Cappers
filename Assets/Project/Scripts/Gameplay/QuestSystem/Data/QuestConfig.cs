using Gameplay.QuestSystem.Quests;
using UnityEngine;

namespace Gameplay.QuestSystem.Data
{
    public abstract class QuestConfig : ScriptableObject
    {
        public string QuestName;

        public abstract QuestType QuestType { get; }

        public abstract Quest CreateQuest(QuestData questData);
        public abstract string GetDescription();
    }
}
