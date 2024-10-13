using UnityEngine;

namespace Gameplay.Ship.Data
{
    [CreateAssetMenu(menuName = "Ship/Config")]
    public class ShipConfig : ScriptableObject
    {
        public ShipPlacementConfig PlacementConfig;
    }
}
