using UnityEngine;
using Gameplay.World.Variants.Port;
using Gameplay.QuestSystem.Data;
using Gameplay.QuestSystem.Quests;

namespace Gameplay.World.Data
{
    [CreateAssetMenu(menuName = "World/Locations/PortLocationConfig")]
    public class PortLocationConfig : LocationConfig
    {
        [SerializeField] private QuestConfig[] quests;

        public QuestConfig GetQuestConfig(int id) => quests[id];

        public override Location CreateLocation(int locationId)
        {
            var questsDatas = new QuestData[quests.Length];
            for (int i = 0; i < quests.Length; i++)
                questsDatas[i] = new QuestData(i, locationId);

            return new PortLocation(locationId, questsDatas);
        }
    }
}
