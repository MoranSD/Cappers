using System.Linq;

namespace Gameplay.World
{
    public class GameWorld
    {
        public readonly int Id;

        private readonly Location[] locations;

        public GameWorld(int id, params Location[] locations)
        {
            Id = id;
            this.locations = locations;
        }

        public Location GetLocation(int id) => locations.FirstOrDefault(p => p.Id == id);
        public bool HasLocation(int id) => locations.Any(p => p.Id == id);
    }
}
