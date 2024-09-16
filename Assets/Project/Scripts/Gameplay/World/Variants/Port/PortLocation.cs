using Gameplay.QuestSystem.Quests;
using Gameplay.World.Data;

namespace Gameplay.World.Variants.Port
{
    public class PortLocation : Location
    {
        public override LocationType Type => LocationType.port;
        public readonly QuestData[] Quests;

        public PortLocation(int id, QuestData[] quests) : base(id)
        {
            Quests = quests;
        }
    }
}
