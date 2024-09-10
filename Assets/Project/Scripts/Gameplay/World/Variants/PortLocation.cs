using UnityEngine;
using World.Data;

namespace World.Variants
{
    public class PortLocation : Location
    {
        public override LocationType Type => LocationType.port;

        public PortLocation(string name, Vector2 position) : base(name, position)
        {
        }
    }
}
