using World.Data;

namespace World.Variants
{
    public class PortLocation : Location
    {
        public override LocationType Type => LocationType.port;

        public PortLocation(int id, string name) : base(id, name)
        {
        }
    }
}
