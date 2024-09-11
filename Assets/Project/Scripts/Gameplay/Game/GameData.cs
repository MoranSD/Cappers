using System.Collections.Generic;
using World;

namespace Gameplay.Game
{
    public class GameData
    {
        public GameWorld World { get; private set; }
        public IReadOnlyList<int> OpenedLocations => openedLocations;

        private List<int> openedLocations;

        public GameData()
        {
            openedLocations = new List<int>();
        }

        public void SetWorld(GameWorld newWorld)
        {
            if (World != null)
                throw new System.Exception();

            if (newWorld == null)
                throw new System.Exception();

            World = newWorld;
        }

        public void OpenLocation(int locationId)
        {
            if (World.HasLocation(locationId) == false)
                throw new System.Exception(locationId.ToString());

            if(openedLocations.Contains(locationId))
                throw new System.Exception(locationId.ToString());

            openedLocations.Add(locationId);
        }
    }
}
