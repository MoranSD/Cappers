using UnityEngine;
using Gameplay.World.Variants;

namespace Gameplay.World.Data
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
