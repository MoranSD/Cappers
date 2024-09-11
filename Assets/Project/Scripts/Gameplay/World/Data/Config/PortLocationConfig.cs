using UnityEngine;
using World.Variants;

namespace World.Data
{
    [CreateAssetMenu(menuName = "World/Locations/PortLocationConfig")]
    public class PortLocationConfig : LocationConfig
    {
        public override Location CreateLocation()
        {
            return new PortLocation(Id, Name);
        }
    }
}
