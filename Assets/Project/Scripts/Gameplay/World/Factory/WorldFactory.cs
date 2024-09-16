using Infrastructure.DataProviding;
using Utils;

namespace Gameplay.World.Factory
{
    public class WorldFactory
    {
        private readonly IAssetProvider assetProvider;

        public WorldFactory(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }

        public GameWorld CreateWorld(int worldId)
        {
            var currentWorldConfig = GameDataProvider.GetWorldConfig(worldId);

            var locations = new Location[currentWorldConfig.LocationsCount];
            for (int i = 0; i < locations.Length; i++) 
                locations[i] = currentWorldConfig.GetLocationConfig(i).CreateLocation(i);

            var gameWorld = new GameWorld(worldId, locations);
            return gameWorld;
        }
    }
}
