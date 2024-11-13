using Gameplay.Game;
using Gameplay.Player.InteractController;
using Gameplay.Ship.Map.View;
using Gameplay.Ship.Map.View.IconsHolder;
using Infrastructure;
using Infrastructure.Composition;
using Infrastructure.DataProviding;
using Gameplay.Panels;
using Gameplay.Travel;
using UnityEngine;
using Gameplay.World.Data;
using System.Threading.Tasks;
using Gameplay.Ship.Map;
using Gameplay.Ship.UnitControl.Placement;
using Gameplay.Ship.UnitControl.Placement.View;
using Gameplay.Ship.Data;
using System.Threading;
using Gameplay.Ship.UnitControl.LifeTime;
using Gameplay.UnitSystem.Factory;

namespace Gameplay.Ship.Root
{
    public class ShipInstaller : Installer
    {
        [SerializeField] private ShipMapView mapView;
        [SerializeField] private ShipUnitPlacementView unitPlacementView;

        private PanelsManager panelsManager;
        private TemporaryGameplayPanel gameplayTemporary;
        private MapIconsHolder iconsHolder;

        private ShipUnitPlacement shipUnitPlacement;
        private ShipMap shipMap;
        private ShipUnitExistenceControl existenceControl;

        public override void Initialize()
        {
            var gameState = ServiceLocator.Get<GameState>();
            var travelSystem = ServiceLocator.Get<TravelSystem>();
            var assetProvider = ServiceLocator.Get<IAssetProvider>();
            var playerInteractor = ServiceLocator.Get<PlayerMenuInteractController>();

            var allWorldsConfig = assetProvider.Load<AllWorldsConfig>(Constants.AllWorldConfigsConfigPath);
            var currentWorldConfig = allWorldsConfig.GetWorldConfig(gameState.World.Id);
            var iconsHolderPrefab = currentWorldConfig.MapIconsHolderPrefab;

            var shipConfig = assetProvider.Load<ShipConfig>(Constants.ShipConfig);

            panelsManager = ServiceLocator.Get<PanelsManager>();
            panelsManager.RegisterPanel(mapView);

            gameplayTemporary = new TemporaryGameplayPanel();
            panelsManager.RegisterPanel(gameplayTemporary);

            iconsHolder = Instantiate(iconsHolderPrefab, mapView.IconsHolderPivot);
            iconsHolder.Initialize(currentWorldConfig);
            mapView.Initialize(iconsHolder);

            shipMap = new ShipMap(gameState, travelSystem, mapView, playerInteractor);

            shipMap.Initialize();

            shipUnitPlacement = new ShipUnitPlacement(unitPlacementView, gameState, shipConfig.PlacementConfig);
            ServiceLocator.Register(shipUnitPlacement);
        }

        public override void AfterInitialize()
        {
            var gameState = ServiceLocator.Get<GameState>();
            var unitFactory = ServiceLocator.Get<IUnitFactory>();

            existenceControl = new ShipUnitExistenceControl(gameState, shipUnitPlacement, unitFactory);
            existenceControl.Initialize();

            ServiceLocator.Register(existenceControl);
        }

        public override void Dispose()
        {
            shipMap.Dispose();

            panelsManager.UnregisterPanel(gameplayTemporary);
            panelsManager.UnregisterPanel(mapView);
            mapView.Dispose();

            iconsHolder.Dispose();

            ServiceLocator.Remove<ShipUnitPlacement>();
            ServiceLocator.Remove<ShipUnitExistenceControl>();
        }
    }

    public class TemporaryGameplayPanel : IPanel
    {
        public PanelType Type => PanelType.gameplay;

        public async Task Hide(CancellationToken token)
        {
            await Task.Delay(0);
        }

        public async Task Show(CancellationToken token)
        {
            await Task.Delay(0);
        }
    }
}
