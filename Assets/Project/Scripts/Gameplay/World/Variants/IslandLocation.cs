using Gameplay.World.Data;

namespace Gameplay.World.Variants
{
    public class IslandLocation : Location
    {
        public override LocationType Type => LocationType.island;

        public IslandLocation(int id) : base(id)
        {
        }
    }
}
