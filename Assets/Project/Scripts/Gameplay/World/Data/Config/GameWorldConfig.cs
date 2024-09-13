using Gameplay.Ship.Map.View.IconsHolder;
using System.Linq;
using UnityEngine;

namespace Gameplay.World.Data
{
    [CreateAssetMenu(menuName = "World/Config")]
    public class GameWorldConfig : ScriptableObject
    {
        [field: SerializeField] public int Id { get; private set; }
        [field: SerializeField] public MapIconsHolder MapIconsHolderPrefab { get; private set; }

        [field: SerializeField] public LocationConfig[] Locations { get; private set; }

        public LocationConfig GetLocationConfig(int id) => Locations.FirstOrDefault(x => x.Id == id);

        public GameWorld CreateWorld()
        {
            var locations = new Location[Locations.Length];

            for (int i = 0; i < locations.Length; i++)
                locations[i] = Locations[i].CreateLocation();

            return new GameWorld(0, locations);
        }
    }
}
