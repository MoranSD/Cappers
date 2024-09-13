using Gameplay.World.Data;

namespace Gameplay.World
{
    public abstract class Location
    {
        public abstract LocationType Type { get; }

        public readonly int Id;
        public readonly string Name;

        public Location(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
