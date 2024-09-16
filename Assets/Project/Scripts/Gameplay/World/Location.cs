using Gameplay.World.Data;

namespace Gameplay.World
{
    public abstract class Location
    {
        public abstract LocationType Type { get; }

        public readonly int Id;

        public Location(int id)
        {
            Id = id;
        }
    }
}
