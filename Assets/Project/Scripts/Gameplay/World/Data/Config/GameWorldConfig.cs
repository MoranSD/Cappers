using Gameplay.Ship.Map.View.IconsHolder;
using UnityEngine;

namespace Gameplay.World.Data
{
    [CreateAssetMenu(menuName = "World/Config")]
    public class GameWorldConfig : ScriptableObject
    {
        [field: SerializeField] public MapIconsHolder MapIconsHolderPrefab { get; private set; }

        public int LocationsCount => locations.Length;

        [SerializeField] private LocationConfig[] locations;

        public LocationConfig GetLocationConfig(int id) => locations[id];
        public bool HasLocationConfig(int id) => id >= 0 && id < LocationsCount;
        public int GetLocationId(LocationConfig locationConfig)
        {
            for (int i = 0; i < locations.Length; i++)
            {
                if(locationConfig == locations[i])
                    return i;
            }

            throw new System.Exception(locationConfig.LocationName);
        }
    }
}
