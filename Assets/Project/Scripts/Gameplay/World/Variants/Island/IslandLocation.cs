using Gameplay.World.Data;

namespace Gameplay.World.Variants.Island
{
    public class IslandLocation : Location
    {
        public override LocationType Type => LocationType.island;

        public IslandLocation(int id) : base(id)
        {
        }
    }
}
