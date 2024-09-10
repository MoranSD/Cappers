namespace World
{
    public class GameWorld
    {
        private readonly Location[] locations;

        public GameWorld(params Location[] locations)
        {
            this.locations = locations;
        }
    }
}
