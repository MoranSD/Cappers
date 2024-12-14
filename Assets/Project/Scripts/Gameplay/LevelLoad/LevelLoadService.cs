using Gameplay.Game;
using Infrastructure;
using Infrastructure.Composition;
using Infrastructure.DataProviding;
using Infrastructure.SceneLoad;
using Gameplay.World.Data;
using Gameplay.Panels;
using System;
using System.Threading;
using Cysharp.Threading.Tasks;

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

        private CancellationTokenSource cancellationTokenSource;

        public LevelLoadService(PanelsManager panelsManager, ISceneLoader sceneLoader, ICompositionController compositionController, GameState gameState, IAssetProvider assetProvider)
        {
            this.panelsManager = panelsManager;
            this.sceneLoader = sceneLoader;
            this.compositionController = compositionController;
            this.gameState = gameState;
            this.assetProvider = assetProvider;
        }

        public void Initialize()
        {
            cancellationTokenSource = new();
        }

        public void Dispose()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }

        public async void LoadLocation(int locationId)
        {
            await LoadLocationAsync(locationId, cancellationTokenSource.Token);
        }

        public async UniTask LoadLocationAsync(int locationId, CancellationToken token)
        {
            if (locationId == GameConstants.SeaLocationId)
            {
                await Load(SceneType.ShipAtSea, token);
            }
            else
            {
                var allWorldsConfig = assetProvider.Load<AllWorldsConfig>(Constants.AllWorldConfigsConfigPath);
                var currentWorldConfig = allWorldsConfig.GetWorldConfig(gameState.World.Id);

                var targetLocationConfig = currentWorldConfig.GetLocationConfig(locationId);

                if (targetLocationConfig == null)
                    throw new System.Exception(locationId.ToString());

                await Load(targetLocationConfig.LocationSceneType, token);
            }
        }

        private async UniTask Load(SceneType sceneType, CancellationToken token)
        {
            OnBeginChangeLocation?.Invoke();
            IsLoading = true;
            await panelsManager.ShowPanelAsync(PanelType.curtain, token);

            if (token.IsCancellationRequested) return;

            compositionController.Dispose();

            await sceneLoader.LoadAsync(sceneType, token);

            if (token.IsCancellationRequested) return;

            compositionController.Initialize();
            await panelsManager.ShowDefaultAsync(token);

            if (token.IsCancellationRequested) return;

            IsLoading = false;
            OnEndChangeLocation?.Invoke();
        }
    }
}
