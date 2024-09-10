using UnityEngine;
using World.Data;

namespace World
{
    public abstract class Location
    {
        public abstract LocationType Type { get; }

        public readonly string Name;
        public readonly Vector2 Position;

        public Location(string name, Vector2 position)
        {
            Name = name;
            Position = position;
        }
    }
}
