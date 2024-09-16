using UnityEngine;
using Gameplay.World.Variants;

namespace Gameplay.World.Data
{
    [CreateAssetMenu(menuName = "World/Locations/IslandLocationConfig")]
    public class IslandLocationConfig : LocationConfig
    {
        public override Location CreateLocation(int locationId)
        {
            return new IslandLocation(locationId);
        }
    }
}
