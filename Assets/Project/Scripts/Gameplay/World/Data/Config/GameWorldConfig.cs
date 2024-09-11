using Gameplay.Ship.Map.View.IconsHolder;
using UnityEngine;

namespace World.Data
{
    [CreateAssetMenu(menuName = "World/Config")]
    public class GameWorldConfig : ScriptableObject
    {
        [field: SerializeField] public int Id { get; private set; }
        [field: SerializeField] public MapIconsHolder MapIconsHolderPrefab { get; private set; }

        [field: SerializeField] public LocationConfig[] Locations { get; private set; }

        public GameWorld CreateWorld()
        {
            var locations = new Location[Locations.Length];

            for (int i = 0; i < locations.Length; i++)
                locations[i] = Locations[i].CreateLocation();

            return new GameWorld(0, locations);
        }
    }
}
