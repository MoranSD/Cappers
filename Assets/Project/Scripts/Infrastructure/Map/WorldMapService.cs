using System.Collections.Generic;
using World;

namespace Infrastructure.Map
{
    public class WorldMapService
    {
        public IReadOnlyList<Location> Locations => visibleLocations;

        private List<Location> visibleLocations;

        public WorldMapService()
        {
            visibleLocations = new List<Location>();
        }

        public void AddLocation(Location location)
        {
            if (visibleLocations.Contains(location))
                throw new System.Exception();

            visibleLocations.Add(location);
        }
    }
}