using UnityEngine;

namespace QuestSystem.Quests.Item
{
    [CreateAssetMenu(menuName = "Quests/AllQuestItemsConfig")]
    public class WorldQuestItemsConfig : ScriptableObject
    {
        [SerializeField] private QuestItemConfig[] configs;

        public QuestItemConfig GetConfig(int itemId) => configs[itemId];
        public int GetItemId(QuestItemConfig itemConfig)
        {
            for (int i = 0; i < configs.Length; i++)
            {
                if (configs[i] == itemConfig)
                    return i;
            }

            throw new System.Exception();
        }
    }
}
