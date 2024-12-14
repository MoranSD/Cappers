using Gameplay.Game;
using Gameplay.Player.InteractController;
using Gameplay.Ship.Map.View.IconsHolder;
using Infrastructure;
using Infrastructure.Composition;
using Infrastructure.DataProviding;
using Gameplay.Panels;
using Gameplay.Travel;
using UnityEngine;
using Gameplay.World.Data;
using Gameplay.Ship.Map;
using Gameplay.Ship.Data;
using System.Threading;
using Gameplay.UnitSystem.Factory;
using Leopotam.Ecs;
using Gameplay.Ship.UnitControl;
using Cysharp.Threading.Tasks;
using Gameplay.Ship.Map.View;

namespace Gameplay.Ship.Root
{
    public class ShipInstaller : Installer
    {
        [SerializeField] private ShipMapView shipMapView;
        [SerializeField] protected ShipViewsLink shipViewsLink;

        private PanelsManager panelsManager;
        private TemporaryGameplayPanel gameplayTemporary;
        private MapIconsHolder iconsHolder;

        private ShipMap shipMap;
        private ShipUnitExistenceControl existenceControl;

        public override void PreInitialize()
        {
            if (shipMapView == null)
                shipMapView = FindObjectOfType<ShipMapView>();
            if (shipViewsLink == null)
                shipViewsLink = FindObjectOfType<ShipViewsLink>();

            var gameState = ServiceLocator.Get<GameState>();
            var travelSystem = ServiceLocator.Get<TravelSystem>();
            var assetProvider = ServiceLocator.Get<IAssetProvider>();
            var playerInteractor = ServiceLocator.Get<PlayerInteractController>();

            var allWorldsConfig = assetProvider.Load<AllWorldsConfig>(Constants.AllWorldConfigsConfigPath);
            var currentWorldConfig = allWorldsConfig.GetWorldConfig(gameState.World.Id);
            var iconsHolderPrefab = currentWorldConfig.MapIconsHolderPrefab;

            var shipConfig = assetProvider.Load<ShipConfig>(Constants.ShipConfig);

            panelsManager = ServiceLocator.Get<PanelsManager>();
            panelsManager.RegisterPanel(shipMapView);

            gameplayTemporary = new TemporaryGameplayPanel();
            panelsManager.RegisterPanel(gameplayTemporary);

            iconsHolder = Instantiate(iconsHolderPrefab, shipMapView.IconsHolderPivot);
            iconsHolder.Initialize(currentWorldConfig);
            shipMapView.Initialize(iconsHolder);

            shipMap = new ShipMap(gameState, travelSystem, shipMapView, playerInteractor);

            shipMap.Initialize();

            foreach (var cannonView in shipViewsLink.CannonViews)
                cannonView.SetInactive();
        }

        public override void Initialize()
        {
            var gameState = ServiceLocator.Get<GameState>();
            var unitFactory = ServiceLocator.Get<IUnitFactory>();
            var assetProvider = ServiceLocator.Get<IAssetProvider>();
            var shipConfig = assetProvider.Load<ShipConfig>(Constants.ShipConfig);

            existenceControl = new ShipUnitExistenceControl(gameState, shipConfig.PlacementConfig, shipViewsLink.ShipUnitExistenceView, unitFactory);
            existenceControl.Initialize();

            ServiceLocator.Register(existenceControl);

            var ecsSystems = ServiceLocator.Get<EcsSystems>();
            ecsSystems.Inject(existenceControl);
        }

        public override void Dispose()
        {
            shipMap.Dispose();

            panelsManager.UnregisterPanel(gameplayTemporary);
            panelsManager.UnregisterPanel(shipMapView);
            shipMapView.Dispose();

            iconsHolder.Dispose();

            ServiceLocator.Remove<ShipUnitExistenceControl>();
        }
    }

    public class TemporaryGameplayPanel : IPanel
    {
        public PanelType Type => PanelType.gameplay;

        public async UniTask Hide(CancellationToken token)
        {
            await UniTask.Delay(0);
        }

        public async UniTask Show(CancellationToken token)
        {
            await UniTask.Delay(0);
        }
    }
}
