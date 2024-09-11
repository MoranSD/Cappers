using UnityEngine;
using World.Variants;

namespace World.Data
{
    [CreateAssetMenu(menuName = "World/Locations/IslandLocationConfig")]
    public class IslandLocationConfig : LocationConfig
    {
        public override Location CreateLocation()
        {
            return new IslandLocation(Id, Name);
        }
    }
}
