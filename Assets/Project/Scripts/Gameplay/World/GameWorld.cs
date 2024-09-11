using System.Linq;

namespace World
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

        public bool HasLocation(int id) => locations.Any(p => p.Id == id);
    }
}
