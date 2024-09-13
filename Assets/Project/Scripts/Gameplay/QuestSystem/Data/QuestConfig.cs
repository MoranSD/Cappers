using Gameplay.QuestSystem.Quests;
using UnityEngine;

namespace Gameplay.QuestSystem.Data
{
    public abstract class QuestConfig : ScriptableObject
    {
        public string QuestName;
        public string QuestDescription;

        public QuestData QuestData;

        public abstract Quest CreateQuest();
    }
}
