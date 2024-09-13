using Gameplay.Game;
using Infrastructure;
using Infrastructure.Composition;
using Infrastructure.Curtain;
using Infrastructure.DataProviding;
using Infrastructure.SceneLoad;
using System.Linq;
using Gameplay.World.Data;

namespace Gameplay.LevelLoad
{
    public class LevelLoadService : ILevelLoadService
    {
        public bool IsLoading { get; private set; }

        private readonly ILoadingCurtain loadingCurtain;
        private readonly ISceneLoader sceneLoader;
        private readonly ICompositionController compositionController;
        private readonly GameState gameState;
        private readonly IAssetProvider assetProvider;

        public LevelLoadService(ILoadingCurtain loadingCurtain, ISceneLoader sceneLoader, ICompositionController compositionController, GameState gameState, IAssetProvider assetProvider)
        {
            this.loadingCurtain = loadingCurtain;
            this.sceneLoader = sceneLoader;
            this.compositionController = compositionController;
            this.gameState = gameState;
            this.assetProvider = assetProvider;
        }

        public void LoadLocation(int locationId)
        {
            if (locationId == GameConstants.SeaLocationId)
            {
                Load(SceneType.ShipAtSea);
            }
            else
            {
                var allWorldsConfig = assetProvider.Load<AllWorldsConfig>(Constants.AllWorldConfigsConfigPath);
                var currentWorldConfig = allWorldsConfig.GetWorldConfig(gameState.World.Id);
                
                var targetLocationConfig = currentWorldConfig.Locations.FirstOrDefault(x => x.Id == locationId);

                if (targetLocationConfig == null)
                    throw new System.Exception(locationId.ToString());

                Load(targetLocationConfig.SceneType);
            }
        }

        private void Load(SceneType sceneType)
        {
            IsLoading = true;
            loadingCurtain.Show();
            compositionController.Dispose();
            sceneLoader.Load(sceneType, OnSceneLoaded);
        }

        private void OnSceneLoaded()
        {
            compositionController.Initialize();
            loadingCurtain.Hide();
            IsLoading = false;
        }
    }
}
