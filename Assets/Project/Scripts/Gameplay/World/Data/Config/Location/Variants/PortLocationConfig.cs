using UnityEngine;
using Gameplay.World.Variants.Port;
using Gameplay.QuestSystem.Data;
using System.Linq;

namespace Gameplay.World.Data
{
    [CreateAssetMenu(menuName = "World/Locations/PortLocationConfig")]
    public class PortLocationConfig : LocationConfig
    {
        [SerializeField] private QuestConfig[] quests;

        public QuestConfig GetQuestConfig(int id) => quests.FirstOrDefault(x => x.QuestData.QuestId == id);

        public override Location CreateLocation()
        {
            return new PortLocation(Id, Name, quests.Select(x => x.QuestData).ToArray());
        }
    }
}
