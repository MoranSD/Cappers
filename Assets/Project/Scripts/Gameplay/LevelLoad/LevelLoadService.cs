using Gameplay.Game;
using Infrastructure;
using Infrastructure.Composition;
using Infrastructure.DataProviding;
using Infrastructure.SceneLoad;
using System.Linq;
using Gameplay.World.Data;
using System.Threading.Tasks;
using Gameplay.Panels;
using System;

namespace Gameplay.LevelLoad
{
    public class LevelLoadService : ILevelLoadService
    {
        public event Action OnBeginChangeLocation;
        public event Action OnEndChangeLocation;

        public bool IsLoading { get; private set; }

        private readonly PanelsManager panelsManager;
        private readonly ISceneLoader sceneLoader;
        private readonly ICompositionController compositionController;
        private readonly GameState gameState;
        private readonly IAssetProvider assetProvider;

        public LevelLoadService(PanelsManager panelsManager, ISceneLoader sceneLoader, ICompositionController compositionController, GameState gameState, IAssetProvider assetProvider)
        {
            this.panelsManager = panelsManager;
            this.sceneLoader = sceneLoader;
            this.compositionController = compositionController;
            this.gameState = gameState;
            this.assetProvider = assetProvider;
        }

        public async void LoadLocation(int locationId) => await LoadLocationAsync(locationId);

        public async Task LoadLocationAsync(int locationId)
        {
            if (locationId == GameConstants.SeaLocationId)
            {
                await Load(SceneType.ShipAtSea);
            }
            else
            {
                var allWorldsConfig = assetProvider.Load<AllWorldsConfig>(Constants.AllWorldConfigsConfigPath);
                var currentWorldConfig = allWorldsConfig.GetWorldConfig(gameState.World.Id);

                var targetLocationConfig = currentWorldConfig.GetLocationConfig(locationId);

                if (targetLocationConfig == null)
                    throw new System.Exception(locationId.ToString());

                await Load(targetLocationConfig.LocationSceneType);
            }
        }

        private async Task Load(SceneType sceneType)
        {
            OnBeginChangeLocation?.Invoke();
            IsLoading = true;
            await panelsManager.ShowPanelAsync(PanelType.curtain);
            compositionController.Dispose();

            await sceneLoader.LoadAsync(sceneType);

            compositionController.Initialize();
            await panelsManager.ShowDefaultAsync();
            IsLoading = false;
            OnEndChangeLocation?.Invoke();
        }
    }
}
